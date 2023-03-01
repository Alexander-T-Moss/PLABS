using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    private GameObject _followedGameObject;
    private Transform _cameraTransform;
    private int _initialCameraOffset = 5;

    public void SetFollowedGameObject(GameObject gameObject)
    {
        _followedGameObject = gameObject;
    }

    void Awake()
    {
        _cameraTransform = gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        _cameraTransform.position = _followedGameObject.transform.position + Vector3.forward * _initialCameraOffset;
        _cameraTransform.LookAt(_followedGameObject.transform);
    }

    private GameObject GetGameObject()
    {
        return gameObject; 
    }

}
