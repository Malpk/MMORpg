using UnityEngine;

public class PvpSkillScoreUI : MonoBehaviour
{
    [SerializeField] private TextUI _text;
    [SerializeField] private PvpSkillScore _score;

    private void OnEnable()
    {
        if (_score)
        {
            SetText(_score.Score);
            _score.OnUpdate += SetText;
        }
    }

    private void OnDisable()
    {
        if(_score)
            _score.OnUpdate -= SetText;
    }

    private void SetText(int score)
    {
        _text.SetText(score.ToString());
    }
}
