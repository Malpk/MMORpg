using UnityEngine;
using TMPro;

public class SkillPanel : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _scoreSkill = 1;
    [SerializeField] private string _lablePrefix;
    [Header("Reference")]
    [SerializeField] private SkillRow _power;
    [SerializeField] private SkillRow _dexterity;
    [SerializeField] private SkillRow _luck;
    [SerializeField] private SkillRow _intelligence;
    [SerializeField] private SkillRow _survival;
    [SerializeField] private SkillRow _body;
    [SerializeField] private TextMeshProUGUI _scoreLable;

    private EntityStats _stats;

    public event System.Action<EntityStats> OnSkillUpdate;

    private void OnValidate()
    {
        _scoreLable?.SetText(_lablePrefix + _scoreSkill.ToString());
    }

    public void SetStats(EntityStats stats)
    {
        _stats = stats;
        _power.SetValue(_stats.Power);
        _dexterity.SetValue(_stats.Dexterity);
        _luck.SetValue(_stats.Luck);
        _intelligence.SetValue(_stats.Intelligence);
        _survival.SetValue(_stats.Survival);
        _body.SetValue(_stats.Body);
    }

    #region Add
    public void AddPower()
    {
        _stats.Power++;
        _power.SetValue(_stats.Power);
        AddSkill();
    }

    public void AddDexterity()
    {
        _stats.Dexterity++;
        _dexterity.SetValue(_stats.Dexterity);
        AddSkill();
    }

    public void AddLuck()
    {
        _stats.Luck++;
        _luck.SetValue(_stats.Luck);
        AddSkill();
    }

    public void AddIntelligence()
    {
        _stats.Intelligence++;
        _intelligence.SetValue(_stats.Intelligence);
        AddSkill();
    }

    public void AddSurvival()
    {
        _stats.Survival++;
        _survival.SetValue(_stats.Survival);
        AddSkill();
    }

    public void AddBody()
    {
        _stats.Body++;
        _body.SetValue(_stats.Body);
        AddSkill();
    }
    #endregion

    private void AddSkill()
    {
        _scoreSkill--;
        _scoreLable.SetText(_lablePrefix + _scoreSkill.ToString());
        if (_scoreSkill == 0)
        {
            SetMode(false);
        }
        OnSkillUpdate?.Invoke(_stats);
    }

    private void SetMode(bool mode)
    {
        _power.SetMode(mode);
        _dexterity.SetMode(mode);
        _luck.SetMode(mode);
        _intelligence.SetMode(mode);
        _survival.SetMode(mode);
        _body.SetMode(mode);
    }
}
