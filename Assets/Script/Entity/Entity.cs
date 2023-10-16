using UnityEngine;

public abstract class Entity : MonoBehaviour, IPvp
{
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

    public event System.Action OnLoad;
    public event System.Action<EntityData> OnSetData;

    public abstract event System.Action OnComplite;

    public int Level => level.Level;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public Vector2Int RangeAttack => hands.AttackRange + Vector2Int.one * body.Attack;
    public EntityBody Body => body;
    public HandHolder Hands => hands;
    public EntityStats Stats => entityStats;
    public GlorySet Glory => glorySet;

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
        var result = SetAttack(target, part, body.Attack + hands.Attack);
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
        Debug.Log(attack.Result);
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
