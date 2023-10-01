using UnityEngine;
using System.Collections.Generic;

public class AttackMenu : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _maxAction = 1;
    [Header("Reference")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;
    [SerializeField] private PartSelectMenu _protect;
    [SerializeField] private PartSelectMenu _attack;
    [SerializeField] private PvpEntityPanel _enemyPanel;

    private List<PartButton> _selectAttack = new List<PartButton>();
    private List<PartButton> _selectProtect = new List<PartButton>();

    public event System.Action<PartBody[], PartBody[]> OnComplite;

    private int CountAction => _selectAttack.Count + _selectProtect.Count;

    private void OnEnable()
    {
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
        _enemy = target;
        _enemyPanel.BindPanel(target);
    }

    public void Close()
    {
        Attack();
        Protect(_selectProtect);
        _attack.Reload();
        _protect.Reload();
    }

    public void Skip()
    {
        _attack.Reload();
        _protect.Reload();
        _player.Skip();
        Protect(_protect.GetPats());
    }

    private void Attack()
    {
        foreach (var attack in _selectAttack)
        {
            _enemy.Body.TakeDamage(_player.Attack, attack.Part);
        }
        _selectAttack.Clear();
    }

    private void Protect(List<PartButton> selects)
    {
        foreach (var protect in selects)
        {
            _enemy.Body.SetProtect(protect.Part, false);
        }
        selects.Clear();
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
    }

    private void DelesectAttack(PartButton part)
    {
        _selectAttack.Remove(part);
    }

    private void DeselectProtect(PartButton part)
    {
        _selectProtect.Remove(part);
    }
    #endregion

}
