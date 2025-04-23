using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private string m_targetTag = "Player";
    [SerializeField]
    private Collider2D m_checkPointCollider;

    private void Awake()
    {
        if (m_checkPointCollider == null)
        {
            m_checkPointCollider = GetComponent<Collider2D>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        
        if (collision.CompareTag(m_targetTag))
        {
            CheckPointRegistry.RegisterCheckPoint(this.transform.position, collision.gameObject.GetComponentInChildren<WandContainer>(), SceneManager.GetActiveScene().buildIndex);
        }
    }
}
