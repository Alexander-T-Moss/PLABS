using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float TimeModifier = 1f;
    public bool Pause = true;

    public float timeSlider;
    public bool timeButton;

    public void pausePlay(bool pause) // Connects to button and assigns it's boolean state to Pause
    {
        Pause = pause;
    }

    public void GetTimeModifier(float timeModifier) // Connects to slider and takes value rounded to 1dp and assigns to TimeModifier
    {
        TimeModifier = (float)Math.Round(timeModifier, 1);
        timeSlider = TimeModifier;
        // You can also use mySlider.maxvalue to set the maximum value ( will be usefull for when play is in different stages of the simulation )
    }

    void modifyTime(float adjustment) // Adjusts timeScale & fixedDeltaTime
    {
        if ((Time.timeScale = adjustment)! < 0)
        {
            Time.timeScale = adjustment; // Change timeScale by adjustment
            Time.fixedDeltaTime = Time.timeScale * .02f; // Update fixedDeltaTime
        }
    }

    private float currentTimeModifier = 1f;

    public void Update() // Calls modifyTime when up/down arrow pressed
    {
        if(Pause)
        {
            modifyTime(0);
            currentTimeModifier = 0f; // Needed so when unpaused the check to updated time will pass 
        }

        else if (currentTimeModifier != TimeModifier) // OPTIMIZATION to only call modifyTime when TimeModifier changes
        {
            modifyTime(TimeModifier);
            currentTimeModifier = TimeModifier;
        }
        

        // NEEDS IMPROVMENT -- BODGE IMPLIMENTATION FOR NORMAL //

        if(Input.GetKeyUp(KeyCode.Space)) // This can be rehandled in a function toggling the pause state moving the check there
        {
            timeButton = !timeButton;
        }

        // END OF BODGE --------------------------------------- //
    }
}