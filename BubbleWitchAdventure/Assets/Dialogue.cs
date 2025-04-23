using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject DialoguePanel;
    public Text DialogueText;
    public string[] dialogue;
    private int index;

    public GameObject contButton;
    public float wordspeed;
    public bool TeraIsClose;



    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && TeraIsClose)
        {
            if (DialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else 
            {
                DialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (DialogueText.text == dialogue[index])
        { 
            contButton.SetActive(true);
        }
    }


    public void zeroText() 
    {
        DialogueText.text = "";
        index = 0;
        DialoguePanel.SetActive(false);

    }

    IEnumerator Typing()
    { 
        foreach(char letter in dialogue[index].ToCharArray())
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(wordspeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);
        if (index < dialogue.Length - 1)
        {
            index++;
            DialogueText.text = "";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            TeraIsClose = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (DialoguePanel == null || DialogueText == null || contButton == null)
        {
            return;
        }

        if (other.CompareTag("Player"))
        {
            TeraIsClose = false;
            zeroText();
        }
    }

}
    