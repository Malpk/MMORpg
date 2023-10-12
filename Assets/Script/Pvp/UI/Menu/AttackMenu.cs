using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class AttackMenu : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _maxAction = 1;
    [Header("Reference")]
    [SerializeField] private Button _applyButtonl;
    [SerializeField] private PartSelectMenu _protect;
    [SerializeField] private PartSelectMenu _attack;
    [SerializeField] private PvpEntityPanel _enemyPanel;

    private Enemy _target;

    public event System.Action<PartType[]> OnAttack;
    public event System.Action<PartType[]> OnProtect;

    private int CountAction => _attack.SelectCount + _protect.SelectCount;


    private void OnEnable()
    {
        _applyButtonl.interactable = CountAction == _maxAction;
        _attack.OnSelect += SelectAttack;
        _protect.OnSelect += SelectProtect;
        _attack.OnDeselect += DelesectAttack;
        _protect.OnDeselect += DeselectProtect;
    }

    private void OnDisable()
    {
        _attack.OnSelect -= SelectAttack;
        _protect.OnSelect -= SelectProtect;
        _attack.OnDeselect -= DelesectAttack;
        _protect.OnDeselect -= DeselectProtect;
    }

    public void BindMenu(Enemy target)
    {
        if (target != _target)
        {
            _target = target;
            _enemyPanel.BindPanel(target);
            _attack.Reload();
        }
    }

    public void Close()
    {
        _target = null;
        if(_attack.SelectCount > 0)
            OnAttack?.Invoke(GetParts(_attack.Selects));
        OnProtect?.Invoke(GetParts(_protect.Selects));
        Reload();
    }

    public void Skip()
    {
        OnProtect?.Invoke(GetParts(_protect.GetPats()));
        Reload();
    }

    public void Reload()
    {
        BindMenu(null);
        _protect.Reload();
    }

    private PartType[] GetParts(List<PartButton> select)
    {
        var list = new List<PartType>();
        foreach (var attack in select)
        {
            list.Add(attack.Part);
        }
        select.Clear();
        return list.ToArray();
    }

    #region Select
    private void SelectAttack(PartButton part)
    {
        if (CountAction > _maxAction)
            _protect.RemoveSelect();
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void SelectProtect(PartButton part)
    {
        if (CountAction > _maxAction)
            _attack.RemoveSelect();
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void DelesectAttack(PartButton part)
    {
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void DeselectProtect(PartButton part)
    {
        _applyButtonl.interactable = CountAction == _maxAction;
    }
    #endregion

}
