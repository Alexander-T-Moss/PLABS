using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectSelecting : MonoBehaviour
{
    private OverlayManager _overlayManager;
    private GameObject _overlayCanvas;

    public GameObject PanelPrefab;
    public GameObject InfoPanel;

    private void OnEnable()
    {
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
        _overlayCanvas = GameObject.Find("OverlayCanvas");
    }

    private void Start()
    {
        InfoPanel = _overlayCanvas.GetComponent<PanelSpawner>().AddPanel(PanelPrefab, gameObject);
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
        _overlayManager.OpenGameObjectPanel(gameObject);
    }
}
