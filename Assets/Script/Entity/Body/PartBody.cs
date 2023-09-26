using UnityEngine;

public class PartBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PartType _type;
    [SerializeField] private BodyPartStateData[] _states;
    [Header("Reference")]
    [SerializeField] private Armor _armor;

    private int _curretHealth;
    private BodyPartStateData _curretState;

    public event System.Action<int, BodyPartStateData> OnUpdateHealth;
    public event System.Action<Armor> OnSetArmor;


    public int Health
    {
        get
        {
            return _curretHealth;
        }
        private set
        {
            _curretHealth = value;
            OnUpdateHealth?.Invoke(_curretHealth, _curretState);
        }
    }

    public int Protect => _armor ? _armor.Protect : 0;
    public PartType Part => _type;

    public Armor Armor => _armor;
    public BodyPartStateData State => _curretState;


    private void OnValidate()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
    }

    private void Start()
    {
        AddArmor(_armor);
    }

    public void SetHealth(int health)
    {
        _health = health;
        Health = health;
    }

    public void TakeDamage(int damage)
    {
        if (_armor)
            damage = Mathf.Clamp(damage - _armor.Protect, 0, damage);
        var stateData = _states[Random.Range(0, _states.Length)];
        if (_curretState == null)
        {
            _curretState = stateData;
        }
        else if (stateData.Seriousness > _curretState.Seriousness)
        {
            _curretState = stateData;
        }
        Health = Health - damage > 0 ? Health - damage : 0;
    }

    public void TakeHeal(int heal)
    {
        var health = Health + heal;
        Health = health > _health ? _health :health;
    }

    public void AddArmor(Armor armor)
    {
        _armor = armor;
        OnSetArmor?.Invoke(armor);
    }
}
