using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpawner : MonoBehaviour
{
    private GameObject _cameraManager;
    public GameObject FollowCamPrefab;

    public void MakeNewFollowCam(GameObject gameObject)
    {
        GameObject cam = Instantiate(FollowCamPrefab);
        FollowCam followCam = cam.GetComponent<FollowCam>();
        cam.name = "Follow Cam";
        cam.transform.SetParent(_cameraManager.transform);
        followCam.SetFollowedGameObject(gameObject);
        gameObject.GetComponent<PhysicsParameters>().SetFollowCam(followCam);
        cam.SetActive(false);
    }

    private void Start()
    {
        _cameraManager = GameObject.Find("CameraManager");
    }
}