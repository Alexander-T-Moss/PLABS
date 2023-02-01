using UnityEngine;
using UnityEngine.UI;

public class PhysicsParameters : MonoBehaviour
{
    // Variables for PhysicsParameters

    // Link to PhysicsEngine
    private GameObject _physicsEngineObject;
    private PhysicsEngineController _physicsEngineController;

    // Modifiable Parameters
    public bool Gravity = false;
    public Vector3 StartingVelocity;
    private Rigidbody _rigidBody;
    private Transform _transform;

    // UserInterface Parameters
    private InputFieldController _posX, _posY, _posZ, _velX, _velY, _velZ, _mass;

    public bool Kinematic = false;


    private void Awake()
    {
        if(gameObject.CompareTag("PhysicsBody"))
            gameObject.GetComponent<Rigidbody>().velocity = StartingVelocity;

        _physicsEngineController = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngineController>();
    }


    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        UpdateParameters();
    }


    private void OnDestroy()
    {
        UpdateParameters(true);
    }


    public void UpdateParameters(bool disableAll = false)
    {
        if(Gravity && !disableAll)
            _physicsEngineController.AddPhysicsBody(gameObject);
        else
            _physicsEngineController.RemovePhysicsBody(gameObject);

        _rigidBody.isKinematic = Kinematic;
        _rigidBody.velocity = new(_velX.GetValue(), _velY.GetValue(), _velZ.GetValue());
        transform.position = new(_posX.GetValue(), _posY.GetValue(), _posZ.GetValue());
    }
}