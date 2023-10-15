using UnityEngine;

public class LevelSet : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _level = 1;
    [Min(2)]
    [SerializeField] private int _levelMax = 100;
    [Header("Score")]
    [SerializeField] private int _score;
    [SerializeField] private int _targetScore = 20;
    [SerializeField] private AnimationCurve _scoreCurve;
    [Header("Reference")]
    [SerializeField] private TextUI _scoreText;

    public event System.Action OnUpLevel;

    public int Score 
    {
        get 
        {
            return _score;
        }
        private set
        {
            _score = value;
            UpdateText();
        }
    }

    public int Level => _level;

    private void OnValidate()
    {
        UpdateText();
    }

    private void Awake()
    {
        UpdateText();
    }

    public SaveLevel Save()
    {
        var save = new SaveLevel();
        save.Level = _level;
        save.Score = Score;
        save.TargetScore = _targetScore;
        return save;
    }

    public void Load(SaveLevel save)
    {
        if (save != null)
        {
            _level = save.Level;
            Score = save.Score;
            _targetScore = save.TargetScore;
            UpdateText();
        }
    }

    #region Score

    public void AddScore(int score)
    {
        Score += score;
        if (Score > _targetScore)
        {
            NextLevel();
        }
    }

    public void NextLevel()
    {
        _level++;
        Score = Mathf.Clamp(Score - _targetScore, 0, Score);
        var target = _targetScore * _scoreCurve.Evaluate(_level / (float)_levelMax);
        _targetScore += (int)target;
        OnUpLevel?.Invoke();
    }

    private void UpdateText()
    {
        _scoreText?.SetText($"{_score}/{_targetScore}");
    }
    #endregion
}
