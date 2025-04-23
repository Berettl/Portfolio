using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private int m_health = 1;

    [SerializeField]
    private UnityEvent m_onDeath;

    // Use To Set Health Without Triggering OnDeath
    public int Health
    {
        get { return m_health; }
        set { m_health = value; }
    }

    public void Damage(int damage)
    {
        if (m_health <= 0)
        {
            return;
        }

        m_health -= damage;

        if (m_health <= 0)
        {
            m_health = 0;
            OnDeath();
        }
    }

    private void OnDeath()
    {
        if (m_onDeath != null)
        {
            m_onDeath.Invoke();
        }
    }
}
