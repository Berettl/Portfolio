using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShadowCast : MonoBehaviour
{
    [SerializeField]
    private Collider2D m_characterCollider;

    [SerializeField]
    private Transform m_shadowObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 bottomPoint = m_characterCollider.ClosestPoint(Vector2.down);

        RaycastHit2D hit = Physics2D.Raycast(bottomPoint, Vector2.down);

        m_shadowObject.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
    }
}
