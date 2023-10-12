using UnityEngine;
using System.Collections.Generic;

public class PartSelectMenu : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _maxSelect = 1;
    [Header("Reference")]
    [SerializeField] private TextUI _contAction;
    [SerializeField] private PartButton[] _partButtons;

    public event System.Action<PartButton> OnSelect;
    public event System.Action<PartButton> OnDeselect;

    private List<PartButton> _selects = new List<PartButton>();

    public int SelectCount => _selects.Count;
    public List<PartButton> Selects => _selects;

    private void Awake()
    {
        _contAction.SetText(_selects.Count.ToString());
    }

    private void OnEnable()
    {
        foreach (var part in _partButtons)
        {
            part.OnSelect += SelectPart;
        }
    }

    private void OnDisable()
    {
        foreach (var part in _partButtons)
        {
            part.OnSelect -= SelectPart;
        }
    }

    public List<PartButton> GetPats()
    {
        var list = new List<PartButton>();
        list.AddRange(_partButtons);
        while (list.Count > _maxSelect && list.Count > 0)
        {
            list.Remove(list[Random.Range(0, list.Count)]);
        }
        return list;
    }

    public bool RemoveSelect()
    {
        Debug.Log("Remove");
        if (_selects.Count > 0)
        {
            Remove(_selects[_selects.Count - 1]);
            return true;
        }
        return false;
    }

    public void Reload()
    {
        foreach (var part in _partButtons)
        {
            part.Reload();
        }
        _selects.Clear();
        _contAction.SetText(_selects.Count.ToString());
    }

    private void SelectPart(PartButton button)
    {
        if (!_selects.Contains(button))
        {
            if (_selects.Count >= _maxSelect)
            {
                Remove(_selects[0]);
            }
            _selects.Add(button);
        }
        OnSelect?.Invoke(button);
        _contAction.SetText(_selects.Count.ToString());
    }

    private void Remove(PartButton part)
    {
        part.Reload();
        _selects.Remove(part);
        OnDeselect?.Invoke(part);
        _contAction.SetText(_selects.Count.ToString());
    }
}
