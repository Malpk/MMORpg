using UnityEngine;
using System.Collections.Generic;

public class EntityBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [Header("Reference")]
    [SerializeField] private Field _field;
    [SerializeField] private BodyPanel _panel;
    [SerializeField] private PartBody[] _parts;

    private int _curretHealth;

    public int Health 
    {
        get
        {
            return _curretHealth;
        }

        private set
        {
            _curretHealth =  value;
            _field?.SetValue((float)_curretHealth / _health);
        }
    }



    private void Reset()
    {
        _parts = GetComponentsInChildren<PartBody>();
    }

    private void OnValidate()
    {
        foreach (var part in _parts)
        {
            part?.SetHealth(_health / _parts.Length);
        }
        Health = GetHealth();
        _panel?.SetArmor(GetAmountArmor());
    }

    public void AddArmor(Armor armor)
    {
        foreach (var part in _parts)
        {
            if (part.Part == armor.Part)
            {
                part.AddArmor(armor);
                _panel.SetArmor(
                    GetAmountArmor());
                return;
            }
        }
    }

    private int GetAmountArmor()
    {
        var amount = 0;
        foreach (var part in _parts)
        {
            amount += part.Protect;
        }
        return amount;
    }

    #region Health

    public void TakeDamage(int damage)
    {
        var parts = GetActivePart();
        if (parts.Count > 0)
        {
            parts[Random.Range(0, parts.Count)].TakeDamage(damage);
            Health = GetHealth();
        }
    }

    public void TekeHeal(int heal = 4)
    {
        if (heal < _parts.Length)
            heal = _parts.Length;
        foreach (var part in _parts)
        {
            part.TakeHeal(heal / _parts.Length);
        }
        Health = GetHealth();
    }


    public bool TekeHeal(int heal, PartType type)
    {
        foreach (var part in _parts)
        {
            if (part.Part == type)
            {
                part.TakeHeal(heal);
                Health = GetHealth();
                return true;
            }
        }
        return false;
    }

    private List<PartBody> GetActivePart()
    {
        var list = new List<PartBody>();
        foreach (var part in _parts)
        {
            if (part.Health > 0)
                list.Add(part);
        }
        return list;
    }

    private int GetHealth()
    {
        var health = 0;
        foreach (var part in _parts)
        {
            health += part.Health;
        }
        return health;
    }
    #endregion
}