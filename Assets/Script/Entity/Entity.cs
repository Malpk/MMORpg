using UnityEngine;

public abstract class Entity : MonoBehaviour, IPvp
{
    [SerializeField] private EntityRang _rang;
    [SerializeField] private EntityData _data;
    [Header("Reference")]
    [SerializeField] protected GlorySet glorySet;
    [SerializeField] protected LevelSet level;
    [SerializeField] protected EntityBody body;
    [SerializeField] protected EntityStats entityStats;

    public event System.Action OnLoad;
    public event System.Action<EntityData> OnSetData;
    
    public abstract event System.Action OnComplite;

    public int Level => level.Level;
    public int Attack => entityStats.Stats.Strenght;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public EntityBody Body => body;
    public LevelSet EntityLevel => level;
    public GlorySet Glory => glorySet;


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

    public SaveEntity Save()
    {
        var save = new SaveEntity();
        save.Glory = glorySet.Glory;
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
        OnLoad?.Invoke();
    }


}
