using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class EntityDataPanel : MonoBehaviour
{
    [SerializeField] private Entity _bind;
    [SerializeField] private List<EntityDataPanel> _childs;
    [Header("UI Reference")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextUI _name;
    [SerializeField] private Field _healthField;
    [SerializeField] private Field _magicField;

    private void OnValidate()
    {
        _childs.Remove(this);
    }

    private void OnEnable()
    {
        if (_bind)
            _bind.Body.OnChangeHealth += _healthField.SetValue;
    }

    private void OnDisable()
    {
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
        if (enabled)
        {
            if (_bind)
            {
                OnDisable();
            }
            OnEnable();
        }
        _bind = entity;
    }

    private void UpdateData()
    {
        _name.SetText(_bind.Name);
        _icon.sprite = _bind.Icon;
        _healthField?.SetValue(_bind.Body.HealthNormalize);
    }
}
