using UnityEngine;
using TMPro;

public class SkillPanel : MonoBehaviour
{
    [SerializeField] private string _lablePrefix;
    [Header("Reference")]
    [SerializeField] private EntityStats _entityStats;
    [Header("UIReference")]
    [SerializeField] private SkillRow _power;
    [SerializeField] private SkillRow _dexterity;
    [SerializeField] private SkillRow _luck;
    [SerializeField] private SkillRow _intelligence;
    [SerializeField] private SkillRow _survival;
    [SerializeField] private SkillRow _body;
    [SerializeField] private SkillRow _attack;
    [SerializeField] private SkillRow _protect;
    [SerializeField] private TextMeshProUGUI _scoreLable;

    private Stats _stats;

    public event System.Action<Stats> OnSkillUpdate;

    private void OnValidate()
    {
        if(_entityStats)
            _entityStats.OnScoreUpdate += UpdateScore;
    }

    private void OnEnable()
    {
        LoadStats();
        _entityStats.OnStatUpdate += LoadStats;
        _entityStats.OnScoreUpdate += UpdateScore;
    }

    private void OnDisable()
    {
        _entityStats.OnStatUpdate -= LoadStats;
        _entityStats.OnScoreUpdate -= UpdateScore;
    }

    #region Add
    public void AddPower()
    {
        _stats.Strenght++;
        _power.SetValue(_stats.Strenght);
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
        if (_entityStats.Score > 0)
        {
            _entityStats.UpdateStats(_stats);
            UpdateScore();
        }
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

    private void UpdateScore()
    {
        SetMode(_entityStats.Score != 0);
        _scoreLable?.SetText(_lablePrefix + _entityStats.Score);
    }

    private void LoadStats()
    {
        SetMode(_entityStats.Score != 0);
        SetStats(_entityStats.Stats);
        UpdateScore();
    }

    private void SetStats(Stats stats)
    {

        _stats = stats;
        _power.SetValue(_stats.Strenght);
        _dexterity.SetValue(_stats.Dexterity);
        _luck.SetValue(_stats.Luck);
        _intelligence.SetValue(_stats.Intelligence);
        _survival.SetValue(_stats.Survival);
        _body.SetValue(_stats.Body);
    }
}
