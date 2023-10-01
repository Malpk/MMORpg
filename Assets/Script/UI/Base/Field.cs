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
    [SerializeField] private List<Field> _child;

    private void Reset()
    {
        _color = Color.red;
        _value = 1f;
        _child.Remove(this);
    }

    private void OnValidate()
    {
        if (_field)
        {
            _field.color = _color;
            SetValue(_value);
        }
        _child.Remove(this);
    }

    public void SetValue(float progress)
    {
        _value = progress;
        _field.fillAmount = _value;
        foreach (var child in _child)
        {
            child?.SetValue(_value);
        }
    }
}
