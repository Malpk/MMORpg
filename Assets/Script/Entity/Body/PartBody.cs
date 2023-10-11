using UnityEngine;
using UnityEngine.Events;

public class PartBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private bool _isProtect;
    [SerializeField] private PartType _type;
    [SerializeField] private BodyPartState _idleState;
    [SerializeField] private BodyPartState[] _states;
    [Header("Reference")]
    [SerializeField] private Armor _armor;
    [SerializeField] private UnityEvent<Item> _onSetArmor;

    private int _curretHealth;
    private BodyPartState _curretState;

    public event System.Action OnLoad;
    public event System.Action<int, BodyPartState> OnUpdateHealth;
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
    public BodyPartState State => _curretState;


    private void OnValidate()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
        _curretState = _idleState;
    }

    private void Awake()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
        _curretState = _idleState;
    }

    private void Start()
    {
        SetArmor(_armor);
    }

    public void SetProtect(bool protect)
    {
        _isProtect = protect;
    }

    #region Health
    public void SetHealth(int health)
    {
        _health = health;
        Health = health;
    }


    public bool TakeDamage(int damage)
    {
        if (!_isProtect)
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

            return true;
        }
        return false;
    }

    public void TakeHeal(int heal)
    {
        var health = Health + heal;
        Health = health > _health ? _health :health;
    }
    #endregion

    #region Save
    public SavePartBody Save()
    {
        var save = new SavePartBody();
        save.FullHealth = _health;
        save.Health = Health;
        save.Part = _type;
        save.State = _curretState.State;
        save.ArmorId = _armor ? _armor.ID : -1;
        return save;
    }

    public void Load(SavePartBody save)
    {
        Health = save.Health;
        _health = save.FullHealth;
        _type = save.Part;
        var state = GetState(save.State);
        var armor = ItemHub.GetItem<Armor>(save.ArmorId);
        if (armor)
            SetArmor(armor);
        if (state)
            _curretState = state;
        OnLoad?.Invoke();
    }

    private BodyPartState GetState(PartState target)
    {
        foreach (var state in _states)
        {
            if (state.State == target)
            {
                return state;
            }
        }
        return null;
    }
    #endregion

    public void SetArmor(Armor armor)
    {
        _armor = armor;
        OnSetArmor?.Invoke(armor);
        _onSetArmor.Invoke(armor);
    }
}
