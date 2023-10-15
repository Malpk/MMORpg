using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _skillScore = 2;
    [SerializeField] private Stats _stats;
    [Header("Reference")]
    [SerializeField] private LevelSet _level;

    public event System.Action OnStatUpdate;
    public event System.Action OnScoreUpdate;

    public int Score { get; private set; } = 0;
    public Stats Stats => _stats;

    private void OnValidate()
    {
        Score = _skillScore;
    }

    private void OnEnable()
    {
        _level.OnUpLevel += UpLevel;
    }

    private void OnDisable()
    {
        _level.OnUpLevel -= UpLevel;
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
            OnStatUpdate?.Invoke();
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
        OnStatUpdate?.Invoke();
    }

    public bool UpdateStats(Stats stats)
    {
        if (Score > 0)
        {
            _stats = stats;
            OnStatUpdate?.Invoke();
            Score--;
            return true;
        }
        return false;
    }

    private void UpLevel()
    {
        var score = _level.Level >= 5 ? _skillScore + 1 : _skillScore;
        AddSkillScore(score);
    }
}
