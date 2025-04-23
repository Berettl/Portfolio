using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnProjectile : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D spawnable;
    [SerializeField]
    private float m_horizontalSpeed = 2f;
    [SerializeField]
    private Vector2 m_offset = Vector2.right;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Spawn(int xDirection)
    {
        Rigidbody2D spawn = Instantiate(spawnable, transform.position + (Vector3)m_offset * xDirection, Quaternion.identity);
        spawn.velocity = new Vector2(xDirection * m_horizontalSpeed, 0);
    }
}
