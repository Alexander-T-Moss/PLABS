using UnityEngine;
using System;
using TMPro;

public class TextFadeInOut : MonoBehaviour
{
    // Variables
    int angle = 0;
    private TMP_Text textElement;
    private Color textColour;

    private void Start()
    {
        textElement = GetComponent<TMP_Text>();
        textColour =  textElement.color;
    }

    private void FixedUpdate()
    {    
        // Use Sin and Abs to vary alpha colour channel
        textColour.a = (float)Math.Abs(Math.Sin(angle * Math.PI/180)); // Change alpha channel to match sine curve
        textElement.color = textColour; // Apply new colour/transparency to textElement
        angle++;

        // Reset angle to prevent creating angle larger than what int can store
        if(angle > 360)
            angle = 0;
    }
}
