using UnityEngine;

public class MainBodyPanel : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private TextUI _attack;
    [SerializeField] private TextUI _protect;

    private void Awake()
    {
        ArmorUpdate();
        AttackUpdate();
        _entity.Body.OnArmorUpdate += ArmorUpdate;
        _entity.Stats.OnStatUpdate += AttackUpdate;
        _entity.Hands.OnUpdateWeapon += (Item weapon) => AttackUpdate();
    }

    private void OnDestroy()
    {
        _entity.Body.OnArmorUpdate -= ArmorUpdate;
        _entity.Stats.OnStatUpdate -= AttackUpdate;
        _entity.Hands.OnUpdateWeapon -= (Item weapon) => AttackUpdate();
    }

    private void AttackUpdate()
    {
        if (_entity.RangeAttack.x != _entity.RangeAttack.y)
            _attack.SetText($"{_entity.RangeAttack.x} - {_entity.RangeAttack.y}");
        else
            _attack.SetText($"{_entity.RangeAttack.x}");
    }

    private void ArmorUpdate()
    {
        int amountProtect = 0;
        foreach (var part in _entity.Body.Parts)
        {
            amountProtect += part.Protect;
        }
        _protect.SetText(amountProtect.ToString());
    }
}
