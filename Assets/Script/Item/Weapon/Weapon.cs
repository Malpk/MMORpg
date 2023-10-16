using UnityEngine;

public class Weapon : Item
{
    [SerializeField] private int _score = 10;
    [SerializeField] private int _maxLevelSkill = 10;
    [SerializeField] private AnimationCurve _levelCurve;
    [Header("Damage")]
    [Range(0, 1f)]
    [SerializeField] private float _skillUnit = 0.05f;
    [SerializeField] private DamageType _damageType;
    [SerializeField] private Vector2Int _attackRamge;

    private Entity _user;

    private float _skill = 0.05f;
    private int _levelSkill = 1;
    private int _curretScore = 0;

    public Vector2Int Attack => _attackRamge;

    public void SetSkill(WeaponSkill skill)
    {
        _skill = skill.Skill;
        _score = skill.Score;
        _levelSkill = skill.Level;
        _curretScore = skill.CurretScore;
    }

    public WeaponSkill GetSkill()
    {
        var skill = new WeaponSkill();
        skill.Skill = _skill;
        skill.Score = _score;
        skill.Level = _levelSkill;
        skill.CurretScore = _curretScore;
        return skill;
    }

    public void AddScore(int score)
    {
        if (_levelSkill < _maxLevelSkill)
        {
            _curretScore += score;
            if (_curretScore > _score)
            {
                _curretScore -= _score;
                _levelSkill++;
                var up = _levelCurve.Evaluate(_levelSkill / (float)_maxLevelSkill);
                _score += (int)(_score * up);
                _skill += _skillUnit;
            }
        }
    }

    public int GetAttack()
    {
        var skill = Random.Range(_skill, 1f);
        var attack = Mathf.Lerp(_attackRamge.x, _attackRamge.y, skill);
        return (int)attack; 
    }

    public override void Pick()
    {
        gameObject.SetActive(false);
    }
    public override void Use(Entity player)
    {
        _user = player;
        _user.Hands.TakeWeapon(this);
    }
    public override void Drop()
    {
        _user.Hands.TakeWeapon(null);
        _user = null;
    }

}
