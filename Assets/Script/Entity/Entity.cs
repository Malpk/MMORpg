using UnityEngine;

public abstract class Entity : MonoBehaviour, IPvp
{
    [Min(0)]
    [SerializeField] private int _manaUnit;
    [Min(0)]
    [SerializeField] private int _healthUnit;
    [SerializeField] private EntityRang _rang;
    [SerializeField] private EntityData _data;
    [Range(0, 1f)]
    [SerializeField] private float _conterAttackProbility;
    [Header("Reference")]
    [SerializeField] private LevelSet level;

    [SerializeField] protected GlorySet glorySet;
    [SerializeField] protected EntityBody body;
    [SerializeField] protected HandHolder hands;
    [SerializeField] protected EntityStats entityStats;

    private int _fullMana;
    private int _fullHealth;
    private int _curretHealth;

    public event System.Action OnLoad;
    public event System.Action OnDead;
    public event System.Action<EntityData> OnSetData;
    public abstract event System.Action OnComplite;
    public event System.Action<float> OnChangeHealth;

    public int Level => level.Level;
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
    public float HealthNormalize => Health / (float)_fullHealth;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public Vector2Int RangeAttack => hands.AttackRange + Vector2Int.one * entityStats.Stats.Strenght;
    public EntityBody Body => body;
    public HandHolder Hands => hands;
    public EntityStats Stats => entityStats;
    public GlorySet Glory => glorySet;

    private void Awake()
    {
        entityStats.OnStatUpdate += UpdateStats;
        entityStats.OnScoreUpdate += UpdateStats;
        body.OnTakeDamage += TakeDamage;
    }

    private void OnDestroy()
    {
        entityStats.OnStatUpdate -= UpdateStats;
        entityStats.OnScoreUpdate -= UpdateStats;
        body.OnTakeDamage -= TakeDamage;
    }

    private void Start()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        _fullMana = entityStats.Stats.Intelligence * _manaUnit;
        _fullHealth = entityStats.Stats.Body * _healthUnit;
        foreach (var part in body.Parts)
        {
            part?.SetHealth(_fullHealth / body.Parts.Length);
        }
        Health = GetHealth();
    }
    private int GetHealth()
    {
        var health = 0;
        foreach (var part in body.Parts)
        {
            health += part.Health;
        }
        return health;
    }

    private void TakeDamage(AttackResult attack)
    {
        if (attack.Damage > 0)
        {
            Health = GetHealth();
            if (Health == 0)
                OnDead?.Invoke();
        }
    }
    #region Save / Load
    public SaveEntity Save()
    {
        var save = new SaveEntity();
        save.Glory = glorySet.Glory;
        save.Hands = hands.Save();
        save.Data = Data;
        save.Body = body?.Save();
        save.Stats = entityStats.Save();
        save.Level = level.Save();
        return save;
    }

    public void Load(SaveEntity save)
    {
        SetData(save.Data);
        glorySet.SetGlroy(save.Glory);
        level.Load(save.Level);
        body?.Load(save.Body);
        entityStats.Load(save.Stats);
        hands.Load(save.Hands);
        if (hands.Weapon)
            hands.Weapon.Use(this);
        foreach (var part in body.Parts)
        {
            if (part.Armor)
                part.Armor.Use(this);
        }
        OnLoad?.Invoke();
    }

    #endregion

    public virtual void Play()
    {
        body.OnTakeDamage -= TryConterAttack;
    }

    public virtual void Stop()
    {
        body.OnTakeDamage += TryConterAttack;
    }

    public void SetData(EntityData data)
    {
        _data = data;
        OnSetData?.Invoke(data);
    }

    public void SetStats(Stats stats)
    {
        entityStats.SetStats(stats);
    }

    #region Attack
    public AttackResult Attack(Entity target, PartType part = PartType.None)
    {
        var result = SetAttack(target, part, entityStats.Stats.Strenght + hands.Attack);
        UpLevel(result);
        return result;
    }

    public AttackResult SetAttack(Entity target, PartType part, int damage)
    {
        var attack = GetAttack(damage);
        if (part != PartType.None)
            return target.body.TakeDamage(attack, part);
        else
            return target.Body.TakeDamage(attack);
    }

    private void TryConterAttack(AttackResult result)
    {
        var probility = Random.Range(0, 1f);
        var evasion = _conterAttackProbility * (entityStats.Stats.Dexterity) / 10f;
        if (probility <= evasion)
        {
            var conterAttack = SetAttack(result.Attaker, PartType.None, result.Damage / 2);
            UpLevel(conterAttack);
        }
    }

    private void UpLevel(AttackResult attack)
    {
        if (attack.Result == AttackType.Full || attack.Result == AttackType.Part)
            hands.AddScore(attack.Damage);
        level.AddScore(attack.Damage);
    }

    private Attack GetAttack(int damage)
    {
       return new Attack(this, damage);
    }
    #endregion


}
