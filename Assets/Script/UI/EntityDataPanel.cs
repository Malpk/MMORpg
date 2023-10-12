using UnityEngine;
using System.Collections.Generic;

public class EntityDataPanel : MonoBehaviour
{
    [SerializeField] private Entity _bind;
    [SerializeField] private List<EntityDataPanel> _childs;
    [Header("UI Reference")]
    [SerializeField] private Field _healthField;
    [SerializeField] private Field _magicField;
    [SerializeField] private ItemPreview _preview;

    public bool IsBusy => _bind;

    private void OnValidate()
    {
        _childs.Remove(this);
    }

    private void Reset()
    {
        enabled = false;
    }

    private void OnEnable()
    {
        if (_bind)
        {
            _bind.Body.OnChangeHealth += _healthField.SetValue;
            _healthField.SetValue(_bind.Body.HealthNormalize);
        }
    }

    private void OnDisable()
    {
        if (_bind)
            _bind.Body.OnChangeHealth -= _healthField.SetValue;
    }

    public void BindPanel(Entity entity)
    {
        SwitchBind(entity);
        foreach (var child in _childs)
        {
            child?.BindPanel(entity);
        }
        UpdateData();
    }

    private void SwitchBind(Entity entity)
    {
        enabled = false;
        _bind = entity;
        enabled = true;
    }

    private void UpdateData()
    {
        _preview.BindEntity(_bind);
        _healthField?.SetValue(_bind ? _bind.Body.HealthNormalize : 1);
    }
}
