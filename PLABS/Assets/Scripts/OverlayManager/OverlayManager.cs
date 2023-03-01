using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    private SimulationManager _simulationManager;
    private PanelSpawner _panelSpawner;
    private TimeController _timeController;   

    private PhysicsParameters _selectedPhysicsBody = null;
    private ParameterPanel _openParameterPanel = null;
    private DataPanel _openDataPanel = null; 

    // POSSIBLE MAKE PRIVATE ?? //
    public GameObject ControlPanel;
    public GameObject EditPanel;

    // Run before first frame update
    private void Start()
    {
        _simulationManager = GameObject.Find("SimulationManager").GetComponent<SimulationManager>();
        _timeController = GameObject.Find("TimeEngine").GetComponent<TimeController>();
        _panelSpawner = GameObject.Find("OverlayCanvas").GetComponent<PanelSpawner>();
    }

    public void SelectGameObject(GameObject gameObject)
    {
        _selectedPhysicsBody = gameObject.GetComponent<PhysicsParameters>();

        if (_simulationManager.GetSimulationState() == "Edit")
            OpenParameterPanel(gameObject);

        else if (_simulationManager.GetSimulationState() == "Running")
            OpenDataPanel(gameObject);
    }

    public ParameterPanel NewParameterPanel(GameObject physicsBody)
    {
        return _panelSpawner.CreateParameterPanel(physicsBody);
    }

    public DataPanel NewDataPanel(GameObject physicsBody)
    {
        return _panelSpawner.CreateDataPanel(physicsBody);
    }
    
    // Open _selectedGameObject parameter panel
    private void OpenParameterPanel(GameObject gameObject)
    {
        CloseParameterPanel();
        _openParameterPanel = _selectedPhysicsBody.GetParameterPanel();
        _selectedPhysicsBody.UpdateParameterPanel();
        _openParameterPanel.SetVisable(true);
    }

    public ParameterPanel GetOpenParameterPanel()
    {
        return _openParameterPanel;
    }
    
    // Close open parameter panel
    private void CloseParameterPanel()
    {
        if(_openParameterPanel != null)
        {
            _openParameterPanel.UpdatePhysicsBody();
            _openParameterPanel.SetVisable(false);
            _openParameterPanel = null;
        }
    }

    // Open gameObject data panel
    private void OpenDataPanel(GameObject gameObject)
    {
        CloseDataPanel();
        _openDataPanel = _selectedPhysicsBody.GetComponent<PhysicsParameters>().GetDataPanel();
        _openDataPanel.SetVisable(true);
    }

    // Close open data panel
    private void CloseDataPanel()
    {
        if(_openDataPanel != null)
        {
            _openDataPanel.SetVisable(false);
            _openDataPanel = null;
        }
    }

    public void SetOverlay(string overlay)
    {
        if(overlay == "Edit")
        {
            CloseDataPanel();
            if(_selectedPhysicsBody != null)
                OpenParameterPanel(_selectedPhysicsBody.GetGameObject());
            ControlPanel.SetActive(false);
            EditPanel.SetActive(true);
            _timeController.SetPause(true);
        }

        else if(overlay == "Running")
        {
            CloseParameterPanel();
            if(_selectedPhysicsBody != null)
                OpenDataPanel(_selectedPhysicsBody.GetGameObject());
            ControlPanel.SetActive(true);
            EditPanel.SetActive(false);
            _timeController.SetPause(false);
        }

        else
            Debug.Log(overlay + " is not a valid overlay display option");
    }

    // Run every frame update
    private void Update()
    {
        // Close open parameter panel on escape key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_simulationManager.GetSimulationState() == "Edit")
            {
                CloseParameterPanel();
                _selectedPhysicsBody = null;
            }

            else if (_simulationManager.GetSimulationState() == "Running")
            {
                CloseDataPanel();
                _selectedPhysicsBody = null;
            }
        }

        if(_simulationManager.GetSimulationState() == "Running")
            if(_selectedPhysicsBody != null)
                _selectedPhysicsBody.UpdateDataPanel();
    }
}