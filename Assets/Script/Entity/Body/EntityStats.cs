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

    private int _score;

    public int Score => _score;
    public Stats Stats => _curretStats;

    private void OnValidate()
    {
        _score = _skillScore;
    }

    private void Awake()
    {
        SetStats(_stats);
        _level.OnUpLevel += UpLevel;
        foreach (var part in _parts)
        {
            part.OnUpdateState += SetCurretStats;
        }
    }

    private void OnDestroy()
    {
        _level.OnUpLevel -= UpLevel;
        foreach (var part in _parts)
        {
            part.OnUpdateState -= SetCurretStats;
        }
    }
    #region Save / Load
    public string Save()
    {
        var data = new EntityStatSave();
        data.Stats = _stats;
        data.SkillScore = _score;
        return JsonUtility.ToJson(data);
    }

    public void Load(string json)
    {
        if (json != "")
        {
            var data = JsonUtility.FromJson<EntityStatSave>(json);
            _score = data.SkillScore;
            _stats = data.Stats;
            SetCurretStats();
        }
    }
    #endregion
    #region Level

    private void UpLevel()
    {
        var score = _level.Level >= 5 ? _skillScore + 1 : _skillScore;
        AddSkillScore(score);
    }

    private void AddSkillScore(int score)
    {
        _score += score;
        OnScoreUpdate?.Invoke();
    }

    #endregion

    public int CheakDebaf(PartType target)
    {
        foreach (var part in _parts)
        {
            if (part.Part == target)
                return part.Level;
        }
        return -1;
    }

    public void SetStats(Stats stats)
    {
        _stats = stats;
        _score = _skillScore;
        SetCurretStats();
    }

    public bool UpdateStats(Stats stats)
    {
        if (_score > 0)
        {
            _stats = stats;
            SetCurretStats();
            _score--;
            return true;
        }
        return false;
    }

    private void SetCurretStats()
    {
        _curretStats = _stats;
        foreach (var part in _parts)
        {
            _curretStats = part.AddDebaf(_curretStats, _stats);
        }
        OnStatUpdate?.Invoke();
    }
}
