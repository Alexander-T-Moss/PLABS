using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataPanel : MonoBehaviour
{
    private PhysicsParameters _physicsBody;
    private TMP_Text _menuHeader, _mass, _velX, _velY, _velZ, _posX, _posY, _posZ;

    private void Awake()
    {
        _menuHeader = GameObject.Find("Menu Header").GetComponent<TMP_Text>();
        _mass = GameObject.Find("Mass").GetComponent<TMP_Text>();
        _velX = GameObject.Find("VelX").GetComponent<TMP_Text>();
        _velY = GameObject.Find("VelY").GetComponent<TMP_Text>();
        _velZ = GameObject.Find("VelZ").GetComponent<TMP_Text>();
        _posX = GameObject.Find("PosX").GetComponent<TMP_Text>();
        _posY = GameObject.Find("PosY").GetComponent<TMP_Text>();
        _posZ = GameObject.Find("PosZ").GetComponent<TMP_Text>();
    }

    public void SetVisable(bool visable)
    {
        gameObject.SetActive(visable);
    }

    public void SetHeader(string header)
    {
        _menuHeader.text = header;
    }

    public void SetMass(float mass)
    {
        _mass.text = string.Format("{0:N2}", mass);
    }

    public void SetVelocity(Vector3 velocity)
    {
        _velX.text = string.Format("{0:N2}", velocity[0]);
        _velY.text = string.Format("{0:N2}", velocity[1]);
        _velZ.text = string.Format("{0:N2}", velocity[2]);
    }

    public void SetPosition(Vector3 position)
    {
        _posX.text = string.Format("{0:N2}", position[0]);
        _posY.text = string.Format("{0:N2}", position[1]);
        _posZ.text = string.Format("{0:N2}", position[2]);
    }

    public void SetPhysicsBody(PhysicsParameters physicsBody)
    {
        _physicsBody = physicsBody;
    }

    public PhysicsParameters GetPhysicsBody()
    {
        return _physicsBody;
    }
}