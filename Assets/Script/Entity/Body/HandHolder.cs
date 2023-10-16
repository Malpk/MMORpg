using System.Collections.Generic;
using UnityEngine;

public class HandHolder : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Armor _shield;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private List<SaveWeaponSkill> _skills = new List<SaveWeaponSkill>();

    public event System.Action<Item> OnUpdateWeapon;
    public event System.Action<Item> OnUpdateShield;

    public int Protect => _shield ? _shield.Protect : 0;
    public int Attack => _weapon ? _weapon.GetAttack() : 0;
    public Vector2Int AttackRange => _weapon ? _weapon.Attack : Vector2Int.zero;

    public Weapon Weapon => _weapon;

    private void Awake()
    {
        if (_weapon)
            SetSkill(_weapon);
    }


    #region Save
    public SaveHandHolder Save()
    {
        var save = new SaveHandHolder();
        if(_shield)
            save.UseShieldId = _shield.ID;
        if(_weapon)
            save.UseWeaponId = _weapon.ID;
        SaveSkill();
        save.Skills = _skills.ToArray();
        return save;
    }

    public void Load(SaveHandHolder save)
    {
        _skills.Clear();
        if (save.Skills != null)
            _skills.AddRange(save.Skills);
        _shield = ItemHub.GetItem<Armor>(save.UseShieldId);
        _weapon = ItemHub.GetItem<Weapon>(save.UseWeaponId);
        if (_weapon)
            SetSkill(_weapon);
        OnUpdateWeapon?.Invoke(_weapon);
    }
    #endregion

    public void AddScore(int score)
    {
        _weapon?.AddScore(score);
    }

    #region WeaponSetup
    public void TakeWeapon(Weapon weapon)
    {
        if (_weapon != weapon)
        {
            SaveSkill();
            _weapon = weapon;
            SetSkill(_weapon);
            OnUpdateWeapon?.Invoke(_weapon);
        }
    }

    private void SetSkill(Weapon weapon)
    {
        if (weapon)
        {
            var skill = GetSkill(weapon.ID);
            if (skill != null)
            {
                weapon.SetSkill(skill.Skill);
            }
        }
    }

    private void SaveSkill()
    {
        if (_weapon)
        {
            var save = GetSkill(_weapon.ID);
            if (save != null)
            {
                save.Skill = _weapon.GetSkill();
            }
            else
            {
                var newSave = new SaveWeaponSkill(_weapon.ID);
                newSave.Skill = _weapon.GetSkill();
                _skills.Add(newSave);
            }
        }
    }
    #endregion



    private SaveWeaponSkill GetSkill(int id)
    {
        foreach (var skill in _skills)
        {
            if (skill.WeaponId == id)
                return skill;
        }
        return null;
    }


}
