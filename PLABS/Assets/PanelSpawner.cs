using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSpawner : MonoBehaviour
{
    public GameObject AddPanel(GameObject panel, GameObject gameObject = null)
    {
        GameObject newPanel = Instantiate(panel);
        newPanel.SetActive(false);
        newPanel.transform.SetParent(this.gameObject.transform, false);
        newPanel.name = gameObject.name + " panel";

        return newPanel;
    }
}
