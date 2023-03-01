using UnityEngine;
using UnityEngine.UI;

public class PhysicsParameters : MonoBehaviour
{
    public float Mass;
    public Vector3 Position;
    public Vector3 Velocity;
    public bool Kinematic = false;

    private Rigidbody _rigidBody;
    private Transform _transform;

    private PhysicsEngine _physicsEngine;
    private OverlayManager _overlayManager;
    private SimulationManager _simulationManager;
    private CameraManager _cameraManager;

    private ParameterPanel _parameterPanel;
    private DataPanel _dataPanel;
    private FollowCam _followCam;

    private void Awake()
    {
        _simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        _physicsEngine = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngine>();
        _cameraManager = GameObject.Find("CameraManager").GetComponent<CameraManager>();

        _rigidBody = gameObject.GetComponent<Rigidbody>();
        _transform = gameObject.GetComponent<Transform>();
    }

    public void InitializePhysicsBody()
    {
        _parameterPanel = _overlayManager.NewParameterPanel(gameObject);
        _dataPanel = _overlayManager.NewDataPanel(gameObject);
        _cameraManager.MakeNewFollowCam(gameObject);
        UpdatePhysicsParameters();
    }


    private void OnEnable()
    {
        Mass = _rigidBody.mass;
        Position = _transform.position;
        Velocity = _rigidBody.velocity;
        Kinematic = _rigidBody.isKinematic;
    }

    // Ran when gameObject is destroyed
    private void OnDestroy()
    {
        if(gameObject.CompareTag("PhysicsBody"))
            _physicsEngine.RemovePhysicsBody(gameObject);
    }

    // Apply parameters to gameObject
    public void UpdateParameters()
    {
        _rigidBody.mass = Mass;
        _rigidBody.velocity = Velocity;
        _rigidBody.isKinematic = Kinematic;
        _transform.position = Position;
        Physics.SyncTransforms();
    }

    public void UpdatePhysicsParameters()
    {
        Mass = _rigidBody.mass;
        Velocity = _rigidBody.velocity;
        Kinematic = _rigidBody.isKinematic;
        Position = _transform.position;
    }

    // Update values in parameter panel
    public void UpdateParameterPanel()
    {
        UpdatePhysicsParameters();
        _parameterPanel.SetMass(_rigidBody.mass);
        _parameterPanel.SetVelocity(_rigidBody.velocity);
        _parameterPanel.SetPosition(transform.position);
    }

    public void UpdateDataPanel()
    {
        _dataPanel.SetMass(_rigidBody.mass);
        _dataPanel.SetVelocity(_rigidBody.velocity);
        _dataPanel.SetPosition(transform.position);
    }

    public void SetFollowCam(FollowCam followCam)
    {
        _followCam = followCam;
    }

    public FollowCam GetFollowCam()
    {
        return _followCam;
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public ParameterPanel GetParameterPanel()
    {
        return _parameterPanel;
    }

    public DataPanel GetDataPanel()
    {
        return _dataPanel;
    }
}