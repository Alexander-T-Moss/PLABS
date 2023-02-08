using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    // Variables
    public bool EditModeState = true;
    private TimeController _timeController;

    private List<GameObject> _editableGameObjects = new();

    private GameObject _selectedGameObject = null;
    private GameObject _openParameterPanel = null;
    private GameObject _openDataPanel = null;

    public GameObject ControlPanel;
    public GameObject EditingPanel;

    public void SelectGameObject(GameObject gameObject)
    {
        _selectedGameObject = gameObject;

        if(EditModeState)
            LoadGameObjectParameterPanel(gameObject);

        else
            LoadGameObjectDataPanel(gameObject);
    }

    public void AddEditableGameObject(GameObject gameObject)
    {
        _editableGameObjects.Add(gameObject);
    }

    public void RemoveEditableGameObject(GameObject gameObject)
    {
        _editableGameObjects.Remove(gameObject);
    }
    
    // Open gameObject parameter panel
    private void LoadGameObjectParameterPanel(GameObject gameObject)
    {
        UnloadGameObjectParameterPanel();
        _openParameterPanel = _selectedGameObject.GetComponent<PhysicsParameters>().GetParameterPanel();
        _openParameterPanel.GetComponent<GameObjectEditingPanel>().GetGameObject().GetComponent<PhysicsParameters>().UpdateParameterPanel();
        _openParameterPanel.SetActive(true);
    }

    // Close open parameter panel
    private void UnloadGameObjectParameterPanel()
    {
        if(_openParameterPanel != null)
        {
            _openParameterPanel.GetComponent<GameObjectEditingPanel>().GetGameObject().GetComponent<PhysicsParameters>().UpdateParameters();
            _openParameterPanel.SetActive(false);
            _openParameterPanel = null;
        }
    }

    // Open gameObject data panel
    private void LoadGameObjectDataPanel(GameObject gameObject)
    {
        UnloadGameObjectDataPanel();
        _openDataPanel = _selectedGameObject.GetComponent<PhysicsParameters>().GetParameterPanel();
        _openDataPanel.SetActive(true);
    }

    // Close open data panel
    private void UnloadGameObjectDataPanel()
    {
        if(_openDataPanel != null)
        {
            _openDataPanel.SetActive(false);
            _openDataPanel = null;
        }
    }

    private void ToggleEditModeState()
    {
        EditModeState = !EditModeState;

        if(EditModeState)
        {
            ControlPanel.SetActive(false);
            EditingPanel.SetActive(true);
            _timeController.SetPause(true);
        }

        else
        {
            ControlPanel.SetActive(true);
            EditingPanel.SetActive(false);
            _timeController.SetPause(false);
        }
    }

    // Run before first frame update
    private void Start()
    {
        ControlPanel.SetActive(false);
        _timeController = GameObject.Find("TimeEngine").GetComponent<TimeController>();
    }

    // Run every frame update
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
            ToggleEditModeState();

        // Close open parameter panel on escape key
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(EditModeState)
                UnloadGameObjectParameterPanel();

            else
                UnloadGameObjectDataPanel();
        }   
    }
}