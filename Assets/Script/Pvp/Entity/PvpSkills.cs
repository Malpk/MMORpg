using UnityEngine;

public class PvpSkills : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private PvpSkillScore[] _skills;

    private void OnEnable()
    {
        _player.Body.OnTakeDamage += TryAdd;
    }

    private void OnDisable()
    {
        _player.Body.OnTakeDamage -= TryAdd;
    }

    public bool AddScore(PvpScoreType type)
    {
        var skill = GetSkill(type);
        skill?.AddScore(1);
        return skill;
    }

    public bool GiveScore(int score, PvpScoreType type)
    {
        var skill = GetSkill(type);
        if (skill)
            return skill.GiveSkore(score);
        return false;
    }

    private void TryAdd(AttackResult result)
    {
        switch (result.Result)
        {
            case AttackType.Evasul:
                AddScore(PvpScoreType.Evasion);
                break;
            case AttackType.Protect:
                AddScore(PvpScoreType.Protect);
                break;
        }
    }

    private PvpSkillScore GetSkill(PvpScoreType type)
    {
        foreach (var skill in _skills)
        {
            if (skill.Type == type)
            {
                return skill;
            }
        }
        return null;
    }
}
