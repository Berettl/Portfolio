using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class Damager : MonoBehaviour
{
    [SerializeField]
    private string m_targetTag;
    [SerializeField]
    private int m_damage = 1;
    [SerializeField]
    private UnityEvent m_onHit;
    [SerializeField]
    private UnityEvent m_onDamage;

    private bool m_isEnabled;

    private void OnDisable()
    {
        m_isEnabled = false;
    }

    private void OnEnable()
    {
        m_isEnabled = true;
    }

    private void DealDamage(HealthController healthController)
    {
        OnHit();

        if (healthController == null)
        {
            return;
        }

        if (healthController.CompareTag(m_targetTag))
        {
            OnDamage();
            healthController.Damage(m_damage);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (m_isEnabled)
        {
            DealDamage(collision.gameObject.GetComponent<HealthController>());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_isEnabled)
        {
            DealDamage(collision.gameObject.GetComponent<HealthController>());
        }
    }

    private void OnHit()
    {
        m_onHit.Invoke();
    }

    private void OnDamage()
    {
        m_onDamage.Invoke();
    }
}
