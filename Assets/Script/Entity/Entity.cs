using UnityEngine;

public abstract class Entity : MonoBehaviour, IPvp
{
    [SerializeField] private EntityRang _rang;
    [SerializeField] private EntityData _data;
    [Header("Reference")]
    [SerializeField] protected GlorySet glorySet;
    [SerializeField] private LevelSet level;
    [SerializeField] protected EntityBody body;
    [SerializeField] protected HandHolder hands;
    [SerializeField] protected EntityStats entityStats;

    public event System.Action OnLoad;
    public event System.Action<EntityData> OnSetData;
    
    public abstract event System.Action OnComplite;

    public int Level => level.Level;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public Vector2Int RangeAttack => hands.Attack + Vector2Int.one * body.Attack;
    public EntityBody Body => body;
    public HandHolder Hands => hands;
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
        OnLoad?.Invoke();
    }

    #endregion

    public abstract void Play();

    public abstract void Stop();

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
    public AttackType Attack(Entity target, PartType part = PartType.None)
    {
        var attack = body.Attack + hands.Weapon.GetAttack();
        var result = SetAttack(target, part, attack);
        if (result == AttackType.Full || result == AttackType.Part)
            hands.Weapon.AddScore(attack);
        level.AddScore(attack);
        return result;
    }

    public AttackType SetAttack(Entity target, PartType part, int attack)
    {
        if (part != PartType.None)
            return target.Body.TakeDamage(attack);
        else
            return target.body.TakeDamage(attack, part);
    }
    #endregion

}
