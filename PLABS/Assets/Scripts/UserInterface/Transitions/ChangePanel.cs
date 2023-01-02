using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePanel : MonoBehaviour
{
    private GameObject thisPanel;

    private void Start()
    {
        thisPanel = this.gameObject;
    }

    public void ChangeToNextPanel(GameObject nextPanel)
    {
        thisPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
}
