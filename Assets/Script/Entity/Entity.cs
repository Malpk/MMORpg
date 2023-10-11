using UnityEngine;

public abstract class Entity : MonoBehaviour, IPvp
{
    [SerializeField] private int _level;
    [SerializeField] private EntityRang _rang;
    [SerializeField] private EntityData _data;
    [Header("Reference")]
    [SerializeField] protected EntityBody body;
    [SerializeField] protected EntityStats entityStats;

    public event System.Action OnLoad;
    public event System.Action<EntityData> OnSetData;
    
    public abstract event System.Action OnComplite;

    public int Level => _level;
    public int Attack => entityStats.Stats.Strenght;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public EntityBody Body => body;

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
        save.Data = Data;
        save.Body = body?.Save();
        save.Stats = entityStats.Save();
        save.Level = Level;
        return save;
    }

    public void Load(SaveEntity save)
    {
        SetData(save.Data);
        _level = save.Level;
        body?.Load(save.Body);
        entityStats.Load(save.Stats);
        OnLoad?.Invoke();
    }


}
