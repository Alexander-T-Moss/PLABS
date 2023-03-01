using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSpawner : MonoBehaviour
{
    public GameObject ParameterPanelPrefab;
    public GameObject DataPanelPrefab;

    private GameObject _controlPanel;
    private GameObject _editingPanel;

    private void Awake()
    {
        _controlPanel = GameObject.Find("Control Panel");
        _editingPanel = GameObject.Find("Editing Panel");

        if(_controlPanel == null || _editingPanel == null)
            Debug.Log("Unable to find panels");
    }

    public ParameterPanel CreateParameterPanel(GameObject physicsBody)
    {
        // Create new panel
        GameObject panel = Instantiate(ParameterPanelPrefab);
        ParameterPanel _parameterPanel = panel.GetComponent<ParameterPanel>();

        // Modify panel
        panel.SetActive(false);
        panel.transform.SetParent(_editingPanel.transform, false);
        panel.name = physicsBody.name + " parameter panel";
        _parameterPanel.SetHeader(physicsBody.name);
        _parameterPanel.SetPhysicsBody(physicsBody.GetComponent<PhysicsParameters>());
        
        // Return modified panel
        return _parameterPanel;
    }

    public DataPanel CreateDataPanel(GameObject physicsBody)
    {
        GameObject panel = Instantiate(DataPanelPrefab);
        DataPanel _dataPanel = panel.GetComponent<DataPanel>();

        panel.SetActive(false);
        panel.transform.SetParent(_controlPanel.transform, false);
        panel.name = physicsBody.name + " data panel";
        _dataPanel.SetHeader(physicsBody.name);
        _dataPanel.SetPhysicsBody(physicsBody.GetComponent<PhysicsParameters>());

        return _dataPanel;
    }
}