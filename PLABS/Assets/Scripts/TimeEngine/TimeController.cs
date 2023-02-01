using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public float TimeModifier = 1f;
    private bool _pauseState = false;
    private bool _tempUnPause;

    private OverlayManager _overlayManager;

    public Toggle PauseToggle;

    // Called when scence is loaded
    private void Awake()
    {
        Time.timeScale = 0f;
        Pause(true);
        PauseToggle = GameObject.Find("PauseToggle").GetComponent<Toggle>();
    }

    private void Start()
    {   
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
    }

    public void SetPause(bool state)
    {
        if(state)
        {
            Time.timeScale = 0f;
            _pauseState = true;
        }

        else
        {
            Time.timeScale = TimeModifier;
            _pauseState = false;
        }
    }

    // Set if the simulation is paused
    public void Pause(bool state, bool tempUnPause = false)
    {
        if(state)
        {
            Time.timeScale = 0f;
            _pauseState = true;
        }

        else
        {
            Time.timeScale = TimeModifier;
            _pauseState = false;
        }

        _tempUnPause = tempUnPause;
    }


    public bool IsPaused()
    {
        return _pauseState;
    }

    public bool WasPaused()
    {
        return _tempUnPause;
    }


    // Adjusts timeScale
    void modifyTime(float adjustment)
    {
        if(!_overlayManager.EditModeState())
        {
            if (Time.timeScale != adjustment && adjustment > 0)
            {
                Time.timeScale = adjustment;
                TimeModifier = adjustment;
            }
            else if(adjustment == 0)
            {
                Pause(true);
            }

            else
                Debug.Log("modifyTime: Not an acceptable value for Time.timeScale");
        }
    } 

    
    private void Update()
    {
        // Pause toggle
        if(Input.GetKeyDown(KeyCode.Space) && !_overlayManager.EditModeState())
            Pause(!_pauseState);

        else if(Input.GetKeyDown(KeyCode.Space) && !_overlayManager.EditModeState())
            Debug.Log("You can't unpause the simulation in edit mode!");

        if(Input.GetKeyDown(KeyCode.UpArrow))
            modifyTime(Time.timeScale + 0.2f);

        else if(Input.GetKeyDown(KeyCode.DownArrow))
            modifyTime(Time.timeScale - 0.2f);
    }
}