using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandBeginner : Wand
{
    [SerializeField]
    private BubbleController m_bubblePrefab;
    [SerializeField]
    private int m_numberOfBubbles = 3;
    [SerializeField]
    protected float m_delay = 0.5f;
    [SerializeField]
    private float m_coolDown = 0.5f;


    override public void WandCast()
    {
        WandCast_Internal(BeginerWandCastRoutine(), m_coolDown);
    }

    /*private void ResetWand()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_coroutine = null;

        m_commsReady = false;
        m_onCoolDown = false;
    }

    public void CommsReady()
    {
        m_commsReady = true;
    }*/

    private IEnumerator BeginerWandCastRoutine()
    {
        for (int i = 0; i < m_numberOfBubbles; i++)
        {
            SummonBubble(m_bubblePrefab, Vector2.zero);

            yield return new WaitForSeconds(m_delay);
        }

        AccessoryFinished();
    }
}
