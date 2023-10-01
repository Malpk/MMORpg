using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Field : MonoBehaviour
{
    [Range(0,1f)]
    [SerializeField] private float _value;
    [SerializeField] private Color _color;
    [Header("Rereference")]
    [SerializeField] private Image _field;

    private void Reset()
    {
        _color = Color.red;
        _value = 1f;
    }

    private void OnValidate()
    {
        if (_field)
        {
            _field.color = _color;
            SetValue(_value);
        }
    }

    public void SetValue(float progress)
    {
        _value = progress;
        _field.fillAmount = _value;
    }
}
