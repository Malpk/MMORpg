using UnityEngine;

public class EntityScore : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private int _timeReload;
    [Header("Reference")]
    [SerializeField] private Field _field;

    private int _curretScore;
    private float _progress = 0f;

    public int Score 
    {
        get
        {
            return _curretScore;
        }
        private set
        {
            if (_curretScore != value)
            {
                _curretScore = value;
                _progress = (float)_curretScore / _score;
                enabled = _progress < 1;
                _field.SetValue(_progress);
            }
        }
    }

    private float timeReload => _timeReload * 60;


    private void Update()
    {
        _progress = Mathf.Clamp01(_progress + Time.deltaTime / timeReload);
        Score = (int)(_score * _progress);
    }

    public void Add(int score)
    {
        Score = Mathf.Clamp(Score + score, Score, _score);
    }

    public void Delete(int score)
    {
        Score = Mathf.Clamp(Score - score, 0, Score);
    }
}
