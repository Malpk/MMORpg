using UnityEngine;


public class EntityStats : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _skillScore = 3;
    [SerializeField] private Stats _stats;

    public event System.Action OnLoad;
    public event System.Action OnScoreUpdate;

    public int Score { get; private set; } = 0;
    public Stats Stats => _stats;

    private void OnValidate()
    {
        Score = _skillScore;
    }

    public string Save()
    {
        var data = new EntityStatSave();
        data.Stats = _stats;
        data.SkillScore = Score;
        return JsonUtility.ToJson(data);
    }

    public void Load(string json)
    {
        if (json != "")
        {
            var data = JsonUtility.FromJson<EntityStatSave>(json);
            Score = data.SkillScore;
            _stats = data.Stats;
            OnLoad?.Invoke();
        }
    }

    public void AddSkillScore(int score)
    {
        Score += score;
        OnScoreUpdate?.Invoke();
    }

    public void SetStats(Stats stats)
    {
        _stats = stats;
        Score = _skillScore;
    }

    public bool UpdateStats(Stats stats)
    {
        if (Score > 0)
        {
            _stats = stats;
            Score--;
            return true;
        }
        return false;
    }
}
