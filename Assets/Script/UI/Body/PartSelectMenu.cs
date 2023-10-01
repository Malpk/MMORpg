using UnityEngine;
using System.Collections.Generic;

public class PartSelectMenu : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _maxSelect = 1;
    [SerializeField] private TextUI _contAction;
    [SerializeField] private PartButton[] _partButtons;

    private EntityBody _body;

    public event System.Action<PartButton> OnSelect;
    public event System.Action<PartButton> OnDeselect;

    private List<PartButton> _selects = new List<PartButton>();

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

    public void SetEntity(EntityBody body)
    {
        _body = body;
    }

    public List<PartButton> GetPats()
    {
        var list = new List<PartButton>();
        while (list.Count > _maxSelect && list.Count > 0)
        {
            list.Remove(list[Random.Range(0, list.Count)]);
        }
        return list;
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
                OnDeselect?.Invoke(_selects[0]);
                _selects[0].Reload();
                _selects.Remove(_selects[0]);
            }
            _selects.Add(button);
            OnSelect?.Invoke(button);
        }
        _contAction.SetText(_selects.Count.ToString());
    }
}
