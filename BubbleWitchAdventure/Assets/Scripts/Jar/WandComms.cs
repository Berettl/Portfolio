using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;


public class WandComms : MonoBehaviour
{
    [SerializeField]
    private UnityEvent m_MagicSignal;
    [SerializeField]
    private Animator m_animator;

    [SerializeField]
    private string m_IsAttackingParamName = "IsAttacking";

    private int m_animIsAttackingHash;

    private Coroutine m_coroutine = null;

    private bool m_wandIsRaised = false;
    private bool m_signalRespondedTo = false;

    private void Awake()
    {
        if (m_animator == null)
        {
            m_animator = GetComponent<Animator>();
        }

        if (m_animator == null)
        {
            throw new MissingComponentException("Missing: Animator");
        }

        m_animIsAttackingHash = Animator.StringToHash(m_IsAttackingParamName);
    }

    private void ResetComms()
    {
        if (m_coroutine != null)
        {
            StopCoroutine(m_coroutine);
        }

        m_animator.SetBool(m_animIsAttackingHash, false);

        m_coroutine = null;
        m_wandIsRaised = false;
        m_signalRespondedTo = false;
    }

    public void Attack()
    {
        if (m_coroutine != null)
        {
            ResetComms();
        }

        m_animator.SetBool(m_animIsAttackingHash, true);
        m_coroutine = StartCoroutine(AttackRoutine());
    }

    private void Raised()
    {
        m_wandIsRaised = true;
    }

    private bool BroadcastSignal()
    {
        if (m_MagicSignal != null)
        {
            m_MagicSignal.Invoke();
            return true;
        }
        return false;
    }

    public void SignalRecieved()
    {
        m_signalRespondedTo = true;
    }

    private IEnumerator AttackRoutine()
    {
        while (!m_wandIsRaised)
        {
            yield return null;
        }

        bool signalBroadcasted = BroadcastSignal();

        while (signalBroadcasted && !m_signalRespondedTo)
        {
            yield return null;
        }

        ResetComms();
    }
}
