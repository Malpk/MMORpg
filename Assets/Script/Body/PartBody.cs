using UnityEngine;
using UnityEngine.Events;

public class PartBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private PartType _type;
    [SerializeField] private BodyPartStateData[] _states;
    [Header("Reference")]
    [SerializeField] private Armor _armor;
    [SerializeField] private BodyPartPanel _panel;
    [Header("Event")]
    [SerializeField] private UnityEvent<Armor> _onArmorUpdate;

    private int _curretHealth;
    private BodyPartStateData _curretState;

    public int Health
    {
        get
        {
            return _curretHealth;
        }
        private set
        {
            _curretHealth = value;
            _panel?.SetHealth(_curretHealth);
        }
    }

    public int Protect => _armor ? _armor.Protect : 0;
    public PartType Part => _type;

    public Armor Armor => _armor;

    private void OnValidate()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
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
        _panel.SetState(_curretState);
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
        _panel?.SetArmor(armor);
        _onArmorUpdate.Invoke(armor);
    }
}
