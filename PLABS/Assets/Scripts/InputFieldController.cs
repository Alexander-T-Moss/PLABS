using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputFieldController : MonoBehaviour
{
    public TMP_InputField _inputField;

    void OnEnable()
    {
        _inputField = gameObject.GetComponent<TMP_InputField>();
        _inputField.contentType = TMP_InputField.ContentType.DecimalNumber;
    }

    public float GetValue()
    {
        return (float) Convert.ToDouble(_inputField.text);
    }

    public void SetValue(float value)
    {
        _inputField.text = value.ToString();
    }
}
