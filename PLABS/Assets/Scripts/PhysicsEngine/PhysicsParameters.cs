using UnityEngine;

public class PhysicsParameters : MonoBehaviour
{
    // Variables for PhysicsParameters

    // Link to PhysicsEngine
    private GameObject _physicsEngineObject;
    private PhysicsEngineController _physicsEngineController;

    // Modifiable Parameters
    public bool Gravity = false;
    public Vector3 StartingVelocity;

    public bool Kinematic = false;


    private void Awake()
    {
        if(gameObject.CompareTag("PhysicsBody"))
            gameObject.GetComponent<Rigidbody>().velocity = StartingVelocity;

        _physicsEngineController = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngineController>();
    }


    private void Start()
    {
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

       gameObject.GetComponent<Rigidbody>().isKinematic = Kinematic;
    }
}