using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_bubbleBody;
    [SerializeField]
    private float m_horizontalSpeed = 10f;
    [SerializeField]
    private float m_verticalSpeed = 10f;
    [SerializeField]
    private float m_duration = 10f;

    [SerializeField]
    private Animator m_bubbleAnimator;
    [SerializeField]
    private string m_triggerPopParmName = "Pop";

    private Coroutine m_coroutine = null;

    private int m_animTriggerPopHash;

    private void Awake()
    {
        if (m_bubbleBody == null)
        {
            m_bubbleBody = GetComponent<Rigidbody2D>();
        }

        if (m_bubbleAnimator == null)
        {
            m_bubbleAnimator = GetComponent<Animator>();
        }

        if (m_bubbleBody == null)
        {
            throw new MissingComponentException("Missing: RigidBody2D");
        }

        if (m_bubbleAnimator == null)
        {
            throw new MissingComponentException("Missing: Animator");
        }

        m_animTriggerPopHash = Animator.StringToHash(m_triggerPopParmName);
    }

    public void Blow(Vector2 direction)
    {
        if (m_coroutine != null)
        {
            return;
        }

        m_bubbleBody.velocity = new Vector2(direction.x * m_horizontalSpeed, direction.y * m_verticalSpeed);

        m_coroutine = StartCoroutine(Decay(m_duration));
    }

    public void Pop()
    {
        m_bubbleAnimator.SetTrigger(m_animTriggerPopHash);
    }

    public void Disperse()
    {
        Destroy(gameObject);
    }

    private IEnumerator Decay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Pop();
    }
}
