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

    private List<PartButton> _selectAttack = new List<PartButton>();
    private List<PartButton> _selectProtect = new List<PartButton>();

    public event System.Action<PartType[]> OnAttack;
    public event System.Action<PartType[]> OnProtect;

    private int CountAction => _selectAttack.Count + _selectProtect.Count;

    private void Awake()
    {
        _applyButtonl.onClick.AddListener(Close);
    }

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
        _enemyPanel.BindPanel(target);
    }

    public void Close()
    {
        if(_selectAttack.Count > 0)
            OnAttack?.Invoke(GetParts(_selectAttack));
        OnProtect?.Invoke(GetParts(_selectProtect));
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
        _attack.Reload();
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
        if (!_selectAttack.Contains(part))
        {
            _selectAttack.Add(part);
            if (CountAction > _maxAction)
            {
                if (_selectProtect.Count > 0)
                {
                    _selectProtect[0].Reload();
                    _selectProtect.Remove(_selectProtect[0]);
                }
                else
                {
                    _selectAttack[0].Reload();
                    _selectAttack.Remove(_selectAttack[0]);
                }
            }
        }
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void SelectProtect(PartButton part)
    {
        if (!_selectProtect.Contains(part))
        {
            _selectProtect.Add(part);
            if (CountAction > _maxAction)
            {
                if (_selectAttack.Count > 0)
                {
                    _selectAttack[0].Reload();
                    _selectAttack.Remove(_selectAttack[0]);
                }
                else
                {
                    _selectProtect[0].Reload();
                    _selectProtect.Remove(_selectProtect[0]);
                }
            }
        }
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void DelesectAttack(PartButton part)
    {
        _selectAttack.Remove(part);
        _applyButtonl.interactable = CountAction == _maxAction;
    }

    private void DeselectProtect(PartButton part)
    {
        _selectProtect.Remove(part);
        _applyButtonl.interactable = CountAction == _maxAction;
    }
    #endregion

}
