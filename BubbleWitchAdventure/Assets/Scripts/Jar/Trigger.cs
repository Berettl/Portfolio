using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    public UnityEvent<Collider2D> onTriggerEnter;
    public UnityEvent<Collider2D> onTriggerExit;
    public UnityEvent<Collider2D> onTriggerStay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (onTriggerEnter != null)
        {
            onTriggerEnter.Invoke(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (onTriggerExit != null)
        {
            onTriggerExit.Invoke(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (onTriggerStay != null)
        {
            onTriggerStay.Invoke(collision);
        }
    }
}
