using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private CameraSpawner _cameraSpawner;

    private void Start()
    {
        _cameraSpawner = gameObject.GetComponent<CameraSpawner>();
    }

    public void MakeNewFollowCam(GameObject followedObject)
    {
        _cameraSpawner.MakeNewFollowCam(followedObject);
    }
}