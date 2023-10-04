using UnityEngine;

public class PvpSkills : MonoBehaviour
{
    [SerializeField] private PvpSkillScore[] _skills;

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
