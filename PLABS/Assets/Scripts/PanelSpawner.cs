using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSpawner : MonoBehaviour
{
    public GameObject AddPanel(GameObject panel, GameObject gameObject)
    {
        GameObject newPanel = Instantiate(panel);
        GameObjectEditingPanel _panelEditor = newPanel.GetComponent<GameObjectEditingPanel>();

        newPanel.SetActive(false);
        newPanel.transform.SetParent(this.gameObject.transform, false);
        newPanel.name = gameObject.name + " parameter panel";
        _panelEditor.SetHeader(gameObject.name);

        return newPanel;
    }
}
