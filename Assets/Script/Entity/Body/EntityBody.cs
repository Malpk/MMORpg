using UnityEngine;
using System.Collections.Generic;

public class EntityBody : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _mana;
    [Min(0)]
    [SerializeField] private int _health;
    [Range(0, 1f)]
    [SerializeField] private float _evasionProbility;
    [Header("Reference")]
    [SerializeField] private PartBody[] _parts;

    private int _curretHealth;

    public event System.Action OnDead;
    public event System.Action<float> OnChangeHealth;

    public int Health
    {
        get
        {
            return _curretHealth;
        }

        private set
        {
            _curretHealth = value;
            OnChangeHealth?.Invoke(HealthNormalize);
        }
    }
    public bool IsDead { get; private set; }

    public float HealthNormalize => Health / (float)_health;

    public PartBody[] Parts => _parts;

    private void Reset()
    {
        _parts = GetComponentsInChildren<PartBody>();
    }

    private void Awake()
    {
        foreach (var part in _parts)
        {
            part?.SetHealth(_health / _parts.Length);
        }
        Health = GetHealth();
    }

    private void OnValidate()
    {
        foreach (var part in _parts)
        {
            part?.SetHealth(_health / _parts.Length);
        }
        Health = GetHealth();
    }

    #region Save
    public string Save()
    {
        var save = new SaveEntityBody();
        save.Mana = _mana;
        save.Evasion = _evasionProbility;
        save.Parts = new SavePartBody[_parts.Length];
        for (int i = 0; i < _parts.Length; i++)
        {
            save.Parts[i] = _parts[i].Save();
        }
        return JsonUtility.ToJson(save);
    }

    public void Load(string json)
    {
        if (json != "")
        {
            var save = JsonUtility.FromJson<SaveEntityBody>(json);
            _mana = save.Mana;
            _evasionProbility = save.Evasion;
            for (int i = 0; i < save.Parts.Length; i++)
            {
                _parts[i].Load(save.Parts[i]);
            }
            Health = GetHealth();
        }
    }
    #endregion
    #region Armor
    public void AddArmor(Armor armor)
    {
        foreach (var part in _parts)
        {
            if (part.Part == armor.Part)
            {
                part.SetArmor(armor);
                return;
            }
        }
    }

    public void RemoveArmor(PartType type)
    {
        var part = GetPart(type);
        part.SetArmor(null);
    }

    public void RemoveArmor(Armor armor)
    {
        var part = GetPart(armor.Part);
        if (part.Armor == armor)
            part.SetArmor(null);
    }

    public bool CheakContaintArmor(PartType type)
    {
        var part = GetPart(type);
        return part.Armor;
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
    #endregion
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
    public AttackType TakeDamage(int damage, PartType target)
    {
        var part = GetPart(target);
        if (part)
        {
            var result = SetDamage(part, damage);
            if (GetActivePart().Count == 0)
                Dead();
            return result;
        }
        return AttackType.None;
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
    public AttackType TakeDamage(int damage)
    {
        var parts = GetActivePart();
        if (parts.Count > 0)
        {
            var part = parts[Random.Range(0, parts.Count)];
            var result = SetDamage(part, damage);
            if (GetActivePart().Count == 0)
                Dead();
            return result;
        }
        return AttackType.None;
    }

    public void Dead()
    {
        IsDead = true;
        OnDead?.Invoke();
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

    private AttackType SetDamage(PartBody part, int damage)
    {
        var evasion = Random.Range(0, 1f);

        if (evasion > _evasionProbility)
        {
            if (part.TakeDamage(damage))
            {
                Health = GetHealth();
                return AttackType.Full;
            }
            else
            {
                return AttackType.Protect;
            }
        }
        return AttackType.Evasul;
    }
}