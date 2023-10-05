using UnityEngine;

public class PvpSkillScore : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private PvpScoreType _type;

    public event System.Action<int> OnUpdate;

    public int Score => _score;
    public PvpScoreType Type => _type;

    public void AddScore(int score)
    {
        _score += score;
        OnUpdate?.Invoke(_score);
    }

    public bool GiveSkore(int score)
    {
        if (_score - score >= 0)
        {
            _score -= score;
            OnUpdate?.Invoke(_score);
            return true;
        }
        return false;
    }
}
