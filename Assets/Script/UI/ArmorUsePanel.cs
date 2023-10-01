using UnityEngine;
using System.Collections.Generic;

public class ArmorUsePanel : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [Header("Reference")]
    [SerializeField] private ArmorPanel[] _panels;
    [SerializeField] private List<ArmorUsePanel> _child;

    private void Awake()
    {
        BindPanel(_entity);
    }

    public void BindPanel(Entity entity)
    {
        _entity = entity;
        var parts = _entity.Body.Parts;
        for (int i = 0; i < _panels.Length; i++)
        {
            _panels[i].SetArmor(parts[i].Armor);
        }
        foreach (var child in _child)
        {
            child.BindPanel(_entity);
        }
    }
}
