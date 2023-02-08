// File Overview -------------------- //
//                                    //
// Handles users clicking on a        //
// GameObject with this script        //
// attached to it                     //
//                                    //
// ---------------------------------- //

using UnityEngine;

public class GameObjectSelector : MonoBehaviour
{
    // Variables
    private OverlayManager _overlayManager;

    // Called once when the gameObject is enabled/waken
    private void Awake()
    {
        // Assign Variables
        _overlayManager = GameObject.Find("OverlayManager").GetComponent<OverlayManager>();
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
       _overlayManager.SelectGameObject(gameObject);
    }
}