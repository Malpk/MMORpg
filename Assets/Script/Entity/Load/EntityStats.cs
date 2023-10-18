using UnityEngine;

public class EntityStats : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _skillScore = 2;
    [SerializeField] private Stats _stats;
    [Header("Reference")]
    [SerializeField] private LevelSet _level;
    [SerializeField] private DebafPart[] _parts;

    public event System.Action OnStatUpdate;
    public event System.Action OnScoreUpdate;

    private Stats _curretStats;

    public int Score { get; private set; } = 0;
    public Stats Stats => _curretStats;

    private void OnValidate()
    {
        Score = _skillScore;
    }

    private void Awake()
    {
        SetStats(_stats);
        _level.OnUpLevel += UpLevel;
        foreach (var part in _parts)
        {
            part.OnUpdateState += UpdateStats;
        }
    }

    private void OnDestroy()
    {
        _level.OnUpLevel -= UpLevel;
        foreach (var part in _parts)
        {
            part.OnUpdateState -= UpdateStats;
        }
    }
    #region Save / Load
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
            _curretStats = data.Stats;
            OnStatUpdate?.Invoke();
        }
    }
    #endregion
    #region Level
    public void AddSkillScore(int score)
    {
        Score += score;
        OnScoreUpdate?.Invoke();
    }

    private void UpLevel()
    {
        var score = _level.Level >= 5 ? _skillScore + 1 : _skillScore;
        AddSkillScore(score);
    }
    #endregion

    public void SetStats(Stats stats)
    {
        _stats = stats;
        _curretStats = stats;
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

    private void UpdateStats()
    {
        _curretStats = _stats;
        foreach (var part in _parts)
        {
            _curretStats = part.AddDebaf(_curretStats);
        }
    }
}
