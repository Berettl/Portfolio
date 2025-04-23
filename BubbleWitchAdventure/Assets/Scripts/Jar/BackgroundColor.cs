using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundColor : MonoBehaviour
{
    public Animator Anim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "DarkBlueFade")
        {
            Anim.SetTrigger("DarkBlueOn");
        }
        if (collision.gameObject.tag == "LightPurpleFade")
        {
            Anim.SetTrigger("LightPurpleOn");
        }
        if (collision.gameObject.tag == "DarkPurpleFade")
        {
            Anim.SetTrigger("DarkPurpleOn");
        }
        if(collision.gameObject.tag == "RedFade")
        {
            Anim.SetTrigger("RedOn");
        }
        if(collision.gameObject.tag == "LightBlueFade")
        {
            Anim.SetTrigger("LightBlueOn");
        }
    }
}
