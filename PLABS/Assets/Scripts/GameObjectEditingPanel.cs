using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectEditingPanel : MonoBehaviour
{
    private GameObject _gameObject;
    public GameObject _menuHeader;

    private InputFieldController _velX, _velY, _velZ, _posX, _posY, _posZ;

    private void Awake()
    {
        _velX = GameObject.Find("VelX InputField").GetComponent<InputFieldController>();
        _velY = GameObject.Find("VelY InputField").GetComponent<InputFieldController>();
        _velZ = GameObject.Find("VelZ InputField").GetComponent<InputFieldController>();
        _posX = GameObject.Find("PosX InputField").GetComponent<InputFieldController>();
        _posY = GameObject.Find("PosY InputField").GetComponent<InputFieldController>();
        _posZ = GameObject.Find("PosZ InputField").GetComponent<InputFieldController>();
    }

    public void SetGameObject(GameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public GameObject GetGameObject()
    {
        return _gameObject;
    }

    // Update header text
    public void SetHeader(string header)
    {
       if(_menuHeader.GetComponent<TextMeshProUGUI>() == null)
            Debug.Log("No Text here");
       else
            _menuHeader.GetComponent<TextMeshProUGUI>().text = header;
    }

    public Vector3 GetVelocity()
    {
        return new(_velX.GetValue(), _velY.GetValue(), _velZ.GetValue());
    }

    public void SetVelocity(Vector3 vector3)
    {
        _velX.SetValue(vector3[0]);
        _velY.SetValue(vector3[1]);
        _velZ.SetValue(vector3[2]);
    }

    public Vector3 GetPosition()
    {
        return new(_velX.GetValue(), _velY.GetValue(), _velZ.GetValue());
    }

    public void SetPosition(Vector3 vector3)
    {
        _posX.SetValue(vector3[0]);
        _posY.SetValue(vector3[1]);
        _posZ.SetValue(vector3[2]);
    }
}