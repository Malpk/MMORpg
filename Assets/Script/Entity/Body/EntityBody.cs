using UnityEngine;
using System.Collections.Generic;

public class EntityBody : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _manaUnit;
    [Min(0)]
    [SerializeField] private int _healthUnit;
    [Range(0, 1f)]
    [SerializeField] private float _evasionProbility;
    [Header("Reference")]
    [SerializeField] private EntityStats _stats;
    [SerializeField] private PartBody[] _parts;

    private int _fullMana;
    private int _fullHealth;
    private int _curretHealth;

    public event System.Action OnDead;
    public event System.Action OnArmorUpdate;
    public event System.Action<AttackResult> OnTakeDamage;
    public event System.Action<float> OnChangeHealth;

    public int Attack => _stats.Stats.Strenght;
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

    public float HealthNormalize => Health / (float)_fullHealth;

    public PartBody[] Parts => _parts;

    private void Reset()
    {
        _parts = GetComponentsInChildren<PartBody>();
    }

    private void Awake()
    {
        UpdateStats();
    }
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

    #region Events
    private void OnEnable()
    {
        _stats.OnStatUpdate += UpdateStats;
        _stats.OnScoreUpdate += UpdateStats;
    }

    private void OnDisable()
    {
        _stats.OnStatUpdate -= UpdateStats;
        _stats.OnScoreUpdate -= UpdateStats;
    }

    private void UpdateStats()
    {
        _fullMana = _stats.Stats.Intelligence * _manaUnit;
        _fullHealth = _stats.Stats.Body * _healthUnit;
        foreach (var part in _parts)
        {
            part?.SetHealth(_fullHealth / _parts.Length);
        }
        Health = GetHealth();
    }

    #endregion
    #region Save / Load
    public string Save()
    {
        var save = new SaveEntityBody();
        save.Mana = _fullMana;
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
            _fullMana = save.Mana;
            _evasionProbility = save.Evasion;
            for (int i = 0; i < save.Parts.Length; i++)
            {
                _parts[i].Load(save.Parts[i]);
            }
            Health = GetHealth();
            OnArmorUpdate?.Invoke();
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
                OnArmorUpdate?.Invoke();
                return;
            }
        }
    }

    public void RemoveArmor(PartType type)
    {
        var part = GetPart(type);
        part.SetArmor(null);
        OnArmorUpdate?.Invoke();
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
    #endregion
    #region Health
  

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
    #region Damage
    public AttackResult TakeDamage(Attack attack, PartType target)
    {
        var part = GetPart(target);
        if (part)
        {
            var result = SetDamage(part, attack);
            OnTakeDamage?.Invoke(result);
            return result;
        }
        return new AttackResult();
    }

    public AttackResult TakeDamage(Attack attack)
    {
        var parts = GetActivePart();
        if (parts.Count > 0)
        {
            var part = parts[Random.Range(0, parts.Count)];
            var result = SetDamage(part, attack);
            OnTakeDamage?.Invoke(result);
            return result;
        }
        return new AttackResult();
    }

    public void Dead()
    {
        IsDead = true;
        OnDead?.Invoke();
    }

    private AttackResult SetDamage(PartBody part, Attack attack)
    {
        if (!TryEvasion(attack))
        {
            if (part.TakeDamage(attack.Damage))
            {
                Health = GetHealth();
                if (Health == 0)
                    Dead();
                return new AttackResult(attack.Attaker, AttackType.Full, attack.Damage);
            }
            else
            {
                return new AttackResult(attack.Attaker, AttackType.Protect);
            }
        }
        return new AttackResult(attack.Attaker, AttackType.Evasul);
    }

    private bool TryEvasion(Attack attack)
    {
        var probility = Random.Range(0, 1f);
        var evasion = _evasionProbility * (_stats.Stats.Dexterity / attack.Power) / 10f;
        return probility <= evasion;
    }
    #endregion

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
}