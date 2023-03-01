using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    private SimulationManager _simulationManager;
    public float TimeModifier = 1f;
    private bool _pauseState = false;
    private bool _tempUnPause;
    private float _timeRate = 1f;

    // Link to OverlayManager
    private OverlayManager _overlayManager;


    public Toggle PauseToggle;

    // Called when scence is loaded
    private void Awake()
    {
        // Find OverlayManager
        _simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        PauseToggle = GameObject.Find("PauseToggle").GetComponent<Toggle>();
    }

    private void Start()
    {
        Pause();
    }

    // Set pause state
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
    public void Pause(bool state = true, bool tempUnPause = false)
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
        if (Time.timeScale != adjustment && adjustment > 0)
        {
            Time.timeScale = adjustment;
            TimeModifier = adjustment;
        }
        else if(adjustment == 0)
        {
            Pause(true);
        }
    } 

    
    private void Update()
    {
        // Pause toggle
        if(Input.GetKeyDown(KeyCode.Space) && _simulationManager.GetSimulationState() == "Running")
            Pause(!_pauseState);

        else if(Input.GetKeyDown(KeyCode.Space))
            Debug.Log("You can't unpause the simulation in edit mode!");

        if(Input.GetKeyDown(KeyCode.UpArrow))
            modifyTime(Time.timeScale + 0.2f);

        else if(Input.GetKeyDown(KeyCode.DownArrow))
            modifyTime(Time.timeScale - 0.2f);
    }
}