using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectSelecting : MonoBehaviour
{
    private OverlayManager _overlayManager;
    private PanelSpawner _panelSpawner;

    public GameObject PanelPrefab;
    public GameObject ParameterPanel;

    private void OnEnable()
    {
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        _panelSpawner = GameObject.Find("OverlayCanvas").GetComponent<PanelSpawner>();
    }

    // OnMouseEnter is called when cursor is over GameObject
    private void OnMouseEnter()
    {
        GetComponent<Renderer>().material.color = Color.grey; 
    }

    // OnMouseExit is called when cursor is no longer over GameObject
    private void OnMouseExit()
    {
        GetComponent<Renderer>().material.color = Color.white;
    }

    // OnMouseDown is called when cursor is clicked over GameObject
    private void OnMouseDown()
    {
        //Debug.Log("Clicked " + gameObject.name);
        //_overlayManager.OpenGameObjectPanel(gameObject);
    }
}
