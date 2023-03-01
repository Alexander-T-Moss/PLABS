using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ParameterPanel : MonoBehaviour
{
    private PhysicsParameters _physicsBody;
    public GameObject _menuHeader;

    private InputFieldController _mass, _velX, _velY, _velZ, _posX, _posY, _posZ;

    private void Awake()
    {
        _mass = GameObject.Find("Mass InputField").GetComponent<InputFieldController>();
        _velX = GameObject.Find("VelX InputField").GetComponent<InputFieldController>();
        _velY = GameObject.Find("VelY InputField").GetComponent<InputFieldController>();
        _velZ = GameObject.Find("VelZ InputField").GetComponent<InputFieldController>();
        _posX = GameObject.Find("PosX InputField").GetComponent<InputFieldController>();
        _posY = GameObject.Find("PosY InputField").GetComponent<InputFieldController>();
        _posZ = GameObject.Find("PosZ InputField").GetComponent<InputFieldController>();
    }

    public void SetVisable(bool visable)
    {
        gameObject.SetActive(visable);
    }
    
    public void SetPhysicsBody(PhysicsParameters physicsBody)
    {
        _physicsBody = physicsBody;
    }
 
    public GameObject GetGameObject()
    {
        return _physicsBody.GetGameObject();
    }

    public void SetHeader(string header)
    {
       if(_menuHeader.GetComponent<TextMeshProUGUI>() == null)
            Debug.Log("No Text here");
       else
            _menuHeader.GetComponent<TextMeshProUGUI>().text = header;
    }

    public float GetMass()
    {
        return _mass.GetValue();
    }

    public void SetMass(float value)
    {
        _mass.SetValue(Mathf.Round(value * 100) * 0.01f);
    }

    public Vector3 GetVelocity()
    {
        return new(_velX.GetValue(), _velY.GetValue(), _velZ.GetValue());
    }

    public void SetVelocity(Vector3 vector3)
    {
        _velX.SetValue(Mathf.Round(vector3[0] * 100) * 0.01f);
        _velY.SetValue(Mathf.Round(vector3[1] * 100) * 0.01f);
        _velZ.SetValue(Mathf.Round(vector3[2] * 100) * 0.01f);
    }

    public Vector3 GetPosition()
    {
        return new(_posX.GetValue(), _posY.GetValue(), _posZ.GetValue());
    }

    public void SetPosition(Vector3 vector3)
    {
        _posX.SetValue(Mathf.Round(vector3[0] * 100) * 0.01f);
        _posY.SetValue(Mathf.Round(vector3[1] * 100) * 0.01f);
        _posZ.SetValue(Mathf.Round(vector3[2] * 100) * 0.01f);
    }

    public void UpdatePhysicsBody()
    {
        _physicsBody.Mass = GetMass();
        _physicsBody.Position = GetPosition();
        _physicsBody.Velocity = GetVelocity();
        _physicsBody.UpdateParameters();
    }
}