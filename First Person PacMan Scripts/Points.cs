using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Points : MonoBehaviour
{
    public int points = 0;
    public TMP_Text Points_UI;

    //If player hits points trigger
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Point"))
        {
            //Add points to score
            points++;
            //Destroy points objects
            Destroy(other.gameObject);

            // Update the UI Text component with the new score
            UpdatePointsUI();
            if(points == 25)
            {
                //Change level if points hits 25
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    private void UpdatePointsUI()
    {
        // Check if a Text component is assigned
        if (Points_UI != null)
        {
            // Update the text with the current score
            Points_UI.text = "Points: " + points.ToString() + "/25";
        }
    }
}
