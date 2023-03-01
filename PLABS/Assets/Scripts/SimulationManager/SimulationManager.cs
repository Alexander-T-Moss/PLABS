using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    // List of all gameObjects in the simulation that are being simulated
    private GameObjectSpawner _gameObjectSpawner;
    private OverlayManager _overlayManager;
    private PhysicsEngine _physicsEngine;
    private PredictionEngine _predictionEngine;
    private CameraSpawner _cameraSpawner;

    private string _simulationState = null;
    private bool _rewinding = false;

    private void Awake()
    {
        _gameObjectSpawner = GetComponent<GameObjectSpawner>();
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        _physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();
        _predictionEngine = GameObject.Find("PredictionEngine").GetComponent<PredictionEngine>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _gameObjectSpawner.MakeNewPhysicsBody("Test Body A");
        _gameObjectSpawner.MakeNewPhysicsBody("Test Body B");
        SetSimulationState("Edit");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M) && _simulationState == "Edit")
            _gameObjectSpawner.MakeNewPhysicsBody();

        if(Input.GetKeyDown(KeyCode.E))
            if(_simulationState == "Edit")
                SetSimulationState("Running");
            else
                SetSimulationState("Edit");

        // Rewinding //
        if(Input.GetKeyDown(KeyCode.Backspace))
            if(_simulationState == "Running")
            {
                Debug.Log("Starting Rewind");
                _rewinding = true;
            }
        if(Input.GetKeyUp(KeyCode.Backspace) && _rewinding)
        {
            Debug.Log("Stopping Rewind");
            _rewinding = false;
        }
        if(_simulationState == "Edit" && _rewinding)
        {
            Debug.Log("Stopping Rewind As Entering Edit Mode");
            _rewinding = false;
        }

        // Trajectory Prediction //
        if(Input.GetKeyDown(KeyCode.Return) && _simulationState == "Running" && !_rewinding)
            _predictionEngine.PredictTrajectories();

        else if(Input.GetKeyDown(KeyCode.Return) && _simulationState == "Edit" && _overlayManager.GetOpenParameterPanel() != null)
            _overlayManager.GetOpenParameterPanel().UpdatePhysicsBody();
    }

    // Check if state is valid
    private bool CheckValidState(string state)
    {
        if(state == "Edit")
            return true;
        else if(state == "Running")
            return true;
        else if(state == "Paused")
            return true;
        else
            return false;
    }

    // Get state of simulation
    public string GetSimulationState()
    {
        return _simulationState;
    }

    // Set state of simulation
    public void SetSimulationState(string state)
    {
        if(CheckValidState(state))
        {
            _simulationState = state;
            _overlayManager.SetOverlay(state);
        }

        else
            Debug.Log("State not valid");
    }

    public bool IsRewinding()
    {
        return _rewinding;
    }
}