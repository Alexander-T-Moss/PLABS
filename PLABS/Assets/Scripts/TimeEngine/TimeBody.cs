using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Profiling;

public class TimeBody : MonoBehaviour
{
    private SimulationManager _simulationManager;

    // Public Variable
    public bool Rewinding = false;

    // Private Variables
    private TimeController _timeController;
    private OverlayManager _overlayManager;
    private Rigidbody _rb;
    private Vector3 _currentVelocity = new();

    // Stack of PointInTime's
    private List<PointInTime> pointsInTime;

    // Called when gameObject this script is attached to is enabled
    private void OnEnable()
    {
        // Assign the simulations TimeController to _timeController
        _simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        _timeController = GameObject.Find("TimeEngine").GetComponent<TimeController>();
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
    }

    // Called before the first frame update
    void Start()
    {
        // Create a new pointsInTime stack
        pointsInTime = new();

        // Assing the gameObjects rigidbody to _rb
        _rb = GetComponent<Rigidbody>();
    }

    // Called every frame update
    void Update()
    {
        if(_simulationManager.IsRewinding() && !Rewinding)
            StartRewind();
        else if (!_simulationManager.IsRewinding() && Rewinding)
            StopRewind();
    }

    void FixedUpdate()
    {
        // Rewind if Rewinding = true
        if (Rewinding)
            Rewind();

        // Record if not rewinding or in edit mode
        else if(_simulationManager.GetSimulationState() == "Running")
            Record();
    }

    void Rewind()
    {
        if (pointsInTime.Count > 0)
        {
            PointInTime pointInTime = pointsInTime[0];
            _currentVelocity = pointInTime.Velocity;
            transform.SetPositionAndRotation(pointInTime.Position, pointInTime.Rotation);
            pointsInTime.RemoveAt(0);
        }
        else
            Debug.Log("No more points to rewind back to!");
    }

    // Records the position of the gameObject using pointsInTime
    void Record()
    {
        // Controls the length of time able to be rewound
        if (pointsInTime.Count > Mathf.Round(60f / Time.fixedDeltaTime))
            pointsInTime.RemoveAt(pointsInTime.Count - 1);

        // Adds the gameObjects current position to pointsInTime
        pointsInTime.Insert(0, new PointInTime(_rb.velocity, transform.position, transform.rotation));
    }

    public void StartRewind()
    {
        Rewinding = true;
        _rb.isKinematic = true;

        if(_timeController.IsPaused())
            _timeController.Pause(false, true);
    }

    public void StopRewind()
    {
        Rewinding = false;
        _rb.velocity = _currentVelocity;

        // Restore kinematic state of gameObject
        _rb.isKinematic = gameObject.GetComponent<PhysicsParameters>().Kinematic;

        // Return simulation to pause state before it was rewinding
        if(_timeController.WasPaused())
            _timeController.Pause(true); 
    }
}