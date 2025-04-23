using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlugasaurousController : MonoBehaviour
{
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private string m_triggerStartParamName = "SlugStart";
    [SerializeField]
    private string m_triggerDeathParamName = "SlugDeath";

    [SerializeField]
    private float m_slowmoMult = 0.5f;

    private int m_animTriggerStartHash;
    private int m_animTriggerDeathHash;

    private void Awake()
    {
        m_animTriggerStartHash = Animator.StringToHash(m_triggerStartParamName);
        m_animTriggerDeathHash = Animator.StringToHash(m_triggerDeathParamName);
    }

    public void StartAnim()
    {
        m_animator.SetTrigger(m_animTriggerStartHash);
    }

    public void OnDeath()
    {
        Time.timeScale = m_slowmoMult;
        m_animator.SetTrigger(m_animTriggerDeathHash);
    }

    public void OnDeathFinished()
    {
        Time.timeScale = 1;
    }
}
