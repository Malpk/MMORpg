using UnityEngine;
using System.Collections.Generic;

public class EntityBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [Header("Reference")]
    [SerializeField] private PartBody[] _parts;

    private int _curretHealth;

    public event System.Action<float> OnChangeHealth;

    public int Health 
    {
        get
        {
            return _curretHealth;
        }

        private set
        {
            _curretHealth =  value;
            OnChangeHealth?.Invoke(HealthNormalize);
        }
    }

    public float HealthNormalize => Health / (float)_health;

    public PartBody[] Parts => _parts;

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
    }

    public void AddArmor(Armor armor)
    {
        foreach (var part in _parts)
        {
            if (part.Part == armor.Part)
            {
                part.AddArmor(armor);
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

    #region PartBody

    public void ReloadPart()
    {
        foreach (var part in _parts)
        {
            part.SetProtect(false);
        }
    }

    public void SetProtect(PartType target, bool protect)
    {
        var part = GetPart(target);
        if (part)
            part.SetProtect(protect);

    }
    public bool TakeDamage(int damage, PartType target)
    {
        var part = GetPart(target);
        if (part)
        {
            part.TakeDamage(damage);
            Health = GetHealth();
            return true;
        }
        return false;
    }
    public bool TekeHeal(int heal, PartType target)
    {
        var part = GetPart(target);
        if (part)
        {
            part.TakeHeal(heal);
            Health = GetHealth();
            return true;
        }
        return false;
    }

    private PartBody GetPart(PartType target, bool isActive = false)
    {
        foreach (var part in _parts)
        {
            if (part.Health > 0 || !isActive)
            {
                if (part.Part == target)
                {
                    return part;
                }
            }
        }
        return null;
    }
    #endregion

    #region Body
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

    #endregion


    private int GetHealth()
    {
        var health = 0;
        foreach (var part in _parts)
        {
            health += part.Health;
        }
        return health;
    }

}