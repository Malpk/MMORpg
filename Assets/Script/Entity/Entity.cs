using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private EntityRang _rang;
    [SerializeField] private EntityData _data;
    [Header("Reference")]
    [SerializeField] protected EntityBody body;
    [SerializeField] protected EntityStats _entityStats;

    public event System.Action<EntityData> OnSetData;

    public int Level => _level;
    public int Attack => _entityStats.Stats.Strenght;
    public EntityRang Rang => _rang;
    public EntityData Data => _data;
    public EntityBody Body => body;

    public void SetData(EntityData data)
    {
        _data = data;
        OnSetData?.Invoke(data);
    }

    public void SetStats(Stats stats)
    {
        _entityStats.SetStats(stats);
    }

    public string Save()
    {
        var save = new PlayerSave();
        save.Data = Data;
        save.Stats = _entityStats.Save();
        save.Level = Level;
        return JsonUtility.ToJson(save);
    }

    public void Load(string json)
    {
        if (json != null)
        {
            var save = JsonUtility.FromJson<PlayerSave>(json);
            SetData(save.Data);
            _level = save.Level;
            _entityStats.Load(save.Stats);
        }
    }


}
