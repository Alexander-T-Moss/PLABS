using System.Collections.Generic;
using UnityEngine;

public class OverlayManager : MonoBehaviour
{
    // Variables
    private GameObject _openObjectPanel = null, _editModePanel, _simulationControlPanel;
    private List<GameObject> _openPanels = new();   

    public bool editMode;
    private TimeController _timeController;

    private void Awake()
    {
        editMode = true;
    }

    private void Start()
    {
        // Assign Variables
        _editModePanel = GameObject.Find("EditMode Panel");
        _timeController = GameObject.Find("TimeEngine").GetComponent<TimeController>();
        _simulationControlPanel = GameObject.Find("SimulationControl Panel");
        _simulationControlPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            CloseCurrentGameObjectPanel();

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Toggle editMode variable
            editMode = !editMode;

            if (editMode)
            {
                _editModePanel.SetActive(true);
                _timeController.SetPause(true);
                _simulationControlPanel.SetActive(false);
            }
            else
            {
                _editModePanel.SetActive(false);
                _timeController.SetPause(false);
                _simulationControlPanel.SetActive(true);
            }
        }
    }

    public bool EditModeState()
    {
        return editMode;
    }

    public void OpenGameObjectPanel(GameObject gameObject)
    {
        CloseCurrentGameObjectPanel();

        _openObjectPanel = gameObject.GetComponent<GameObjectSelecting>().InfoPanel;
        _openObjectPanel.SetActive(true);

        _openPanels.Add(_openObjectPanel);
    }

    public void CloseCurrentGameObjectPanel()
    {
        if (_openObjectPanel != null)
        {
            _openObjectPanel.SetActive(false);
            _openPanels.Remove(_openObjectPanel);
        }
    }

    // Opens a panel based on it's name
    public void OpenPanel(string panelName)
    {
        GameObject.Find(panelName).SetActive(true);
    }

    // Closes a panel based on it's name
    public void ClosePanel(string panelName)
    {
        GameObject.Find(panelName).SetActive(false);
    }

    // Closes all panels in _openPanels
    public void CloseAllPanels()
    {
        for (int x = 0; x < _openPanels.Count; x++)
        {
            _openPanels[x].SetActive(false);
        }
    }
}