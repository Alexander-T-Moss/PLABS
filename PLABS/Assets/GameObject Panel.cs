using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPanel : MonoBehaviour
{
    private void Update()
    {
        // Close (SetInActive) this panel when escape is pressed
        if(Input.GetKeyDown(KeyCode.Escape))
            gameObject.SetActive(false);
    }
}
