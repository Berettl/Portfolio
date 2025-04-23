using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Wand : MonoBehaviour
{
    [SerializeField]
    protected WandContainer m_wandContainer;
    [SerializeField]
    protected Vector2 m_offset;


    [SerializeField]
    protected WandComms m_wandComms;

    protected bool m_coroutineRunning = false;

    protected bool m_commsReady = false;
    protected bool m_accessoryRunning = false;
    protected bool m_onCoolDown = false;

    protected Coroutine m_coroutine;

    abstract public void WandCast();

    virtual protected void WandCast_Internal(IEnumerator accessoryRoutine, float coolDown)
    {
        if (m_onCoolDown)
        {
            return;
        }

        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_coroutine = StartCoroutine(CoreWandCastRoutine(accessoryRoutine, coolDown));
    }

    public void CommsReady()
    {
        if (m_coroutineRunning)
        {
            m_commsReady = true;
        }
    }

    protected void SummonBubble(BubbleController bubblePrefab, Vector2 offset)
    {
        BubbleController bubbleController = Instantiate(bubblePrefab,
                                                        new Vector3(transform.position.x + m_wandContainer.GetDirection().x * (m_offset.x + offset.x),
                                                                    transform.position.y + offset.y, transform.position.z),
                                                        Quaternion.identity);

        bubbleController.Blow(m_wandContainer.GetDirection());
    }

    protected void AccessoryFinished()
    {
        m_accessoryRunning = false;
    }

    protected void ResetWand()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_coroutine = null;
        m_coroutineRunning = false;

        m_commsReady = false;
        m_onCoolDown = false;
        m_accessoryRunning = false;
    }

    protected IEnumerator CoreWandCastRoutine(IEnumerator accessoryRoutine, float coolDown)
    {
        m_coroutineRunning = true;
        m_onCoolDown = true;

        m_wandComms.Attack();
        while (!m_commsReady)
        {
            yield return null;
        }

        if (accessoryRoutine != null)
        {
            m_accessoryRunning = true;

            StartCoroutine(accessoryRoutine);
        }

        while (m_accessoryRunning)
        {
            yield return null;
        }

        m_wandComms.SignalRecieved();

        yield return new WaitForSeconds(coolDown);

        ResetWand();
    }
}
