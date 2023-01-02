using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float TimeModifier = 1f;
    public bool Pause = false;
    private float currentTimeModifier = 1f;

    public void SetPauseState(bool pause)
    {
        Pause = pause;
    }

    public bool GetPauseState()
    {
        return Pause;
    }

    void modifyTime(float adjustment) // Adjusts timeScale & fixedDeltaTime
    {
        if (Time.timeScale != adjustment && adjustment >= 0)
        {
            Time.timeScale = adjustment; // Change timeScale by adjustment
            Time.fixedDeltaTime = Time.timeScale * .02f; // Update fixedDeltaTime
        }
    } 


    private void Update() // Calls modifyTime when up/down arrow pressed
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
    }
}