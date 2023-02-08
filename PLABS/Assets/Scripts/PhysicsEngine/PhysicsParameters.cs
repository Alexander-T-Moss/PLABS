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
    public GameObject ParameterPanelPrefab;
    private GameObject _parameterPanel;
    private Transform _editingPanel;

    public bool Kinematic = false;


    private void Awake()
    {
        // Checks if gameObject isn't a clone
        if(gameObject.CompareTag("PhysicsBody"))
        {
            gameObject.GetComponent<Rigidbody>().velocity = StartingVelocity;
            _editingPanel = GameObject.Find("Editing Panel").GetComponent<Transform>();
            _parameterPanel = Instantiate(ParameterPanelPrefab, _editingPanel);
            _parameterPanel.name = gameObject.name + " ParameterPanel";
            GameObjectEditingPanel gameObjectEditingPanel = _parameterPanel.GetComponent<GameObjectEditingPanel>();
            gameObjectEditingPanel.SetHeader(gameObject.name);
            gameObjectEditingPanel.SetGameObject(gameObject);
            _parameterPanel.SetActive(false);
        }

        _physicsEngineController = GameObject.Find("PhysicsEngine").GetComponent<PhysicsEngineController>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }


    private void Start()
    {
        _rigidBody = gameObject.GetComponent<Rigidbody>();
        UpdateParameters();
    }


    private void OnDestroy()
    {
        // Reset parameters
        UpdateParameters(true);
    }


    public void UpdateParameters(bool disableAll = false)
    {
        if(Gravity && !disableAll)
            _physicsEngineController.AddPhysicsBody(gameObject);
        else
            _physicsEngineController.RemovePhysicsBody(gameObject);

        _rigidBody.isKinematic = Kinematic;

        Debug.Log("Updated parameters for: " + gameObject.name);

        //GameObjectEditingPanel gameObjectEditingPanel = _parameterPanel.GetComponent<GameObjectEditingPanel>();
        //GetComponent<Rigidbody>().velocity = gameObjectEditingPanel.GetVelocity();
        //GetComponent<Transform>().position = gameObjectEditingPanel.GetPosition();
    }


    public void UpdateParameterPanel()
    {
        GameObjectEditingPanel gameObjectEditingPanel = _parameterPanel.GetComponent<GameObjectEditingPanel>();
        gameObjectEditingPanel.SetVelocity(GetComponent<Rigidbody>().velocity);
        gameObjectEditingPanel.SetPosition(GetComponent<Transform>().position);
        Debug.Log("Updated parameter panel for: " + gameObject.name);
    }


    public GameObject GetParameterPanel()
    {
        return _parameterPanel;
    }
}