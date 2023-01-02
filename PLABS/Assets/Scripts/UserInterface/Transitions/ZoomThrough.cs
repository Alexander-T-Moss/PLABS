using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoomThrough : MonoBehaviour
{
    // Panels involved in transition
    GameObject currentPanel;
    RectTransform currentPanelTransform;

    public GameObject nextPanel;
    RectTransform nextPanelTransform;

    // Variables for transition  
    float zoomScale = 0;
    public bool transitioning = false;

    // Initialize transition
    private void Start()
    {
        currentPanel = GetComponent<GameObject>();
        currentPanelTransform = GetComponent<RectTransform>();
        nextPanelTransform = nextPanel.GetComponent<RectTransform>();
    }

    // Call the transition to be run
    public void ZoomThroughPanel(GameObject panelToZoomTo)
    {
        transitioning = true;
        nextPanel = panelToZoomTo;
        nextPanelTransform = nextPanel.GetComponent<RectTransform>();
        nextPanelTransform.localScale = Vector3.zero;
    }

    private void FixedUpdate()
    {
        // Transition code
        if(transitioning)
        {
            // Maths to control speed of scaling/zoom
            zoomScale += 0.01f;
            currentPanelTransform.localScale = 100f * (float)Math.Pow(zoomScale, 10) * Vector3.one + Vector3.one;

            // Check when to stop zoomthrough animation
            if(currentPanelTransform.localScale[2] > 70f)
            {
                transitioning = false; // Stop transition of this panel
                currentPanel.SetActive(false); // Set this panel to inactive
                nextPanel.SetActive(true);
            }
        }
    }
}
