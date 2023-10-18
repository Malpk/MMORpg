using UnityEngine;
using UnityEngine.Events;

public class PartBody : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private bool _isProtect;
    [SerializeField] private PartType _type;
    [Header("Reference")]
    [SerializeField] private Armor _armor;
    [SerializeField] private DebafPart _partState;
    [SerializeField] private UnityEvent<Item> _onSetArmor;

    [SerializeField] private int _curretDamage;
    [SerializeField] private int _curretHealth;

    public event System.Action OnLoad;
    public event System.Action<int> OnUpdateHealth;
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
            OnUpdateHealth?.Invoke(_curretHealth);
        }
    }

    public int Protect => _armor ? _armor.Protect : 0;
    public PartType Part => _type;

    public Armor Armor => _armor;

    public DebafPart State => _partState;

    private void OnValidate()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
    }

    private void Awake()
    {
        if (_armor)
        {
            if (_armor.Part != Part)
                _armor = null;
        }
    }

    private void Start()
    {
        SetArmor(_armor);
    }


    #region Save
    public SavePartBody Save()
    {
        var save = new SavePartBody();
        save.FullHealth = _health;
        save.Health = Health;
        save.Damage = _curretDamage;
        save.Part = _type;
        save.ArmorId = _armor ? _armor.ID : -1;
        save.State = _partState.Save();
        return save;
    }

    public void Load(SavePartBody save)
    {
        _type = save.Part;
        _health = save.FullHealth;
        _curretDamage = save.Damage;
        Health = save.Health;
        var armor = ItemHub.GetItem<Armor>(save.ArmorId);
        if (armor)
            SetArmor(armor);
        _partState.Load(save.State);
        OnLoad?.Invoke();
    }
    #endregion
    #region Health


    public void SetHealth(int health)
    {
        _health = health;
        Health = _health - _curretDamage;
    }

    public bool TakeDamage(int damage)
    {
        if (!_isProtect)
        {
            if (_armor)
                damage = Mathf.Clamp(damage - _armor.Protect, 0, damage);
            _curretDamage = Mathf.Clamp(_curretDamage + damage, 0 , _health);
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
    public void SetProtect(bool protect)
    {
        _isProtect = protect;
    }

    public void SetArmor(Armor armor)
    {
        _armor = armor;
        OnSetArmor?.Invoke(armor);
        _onSetArmor.Invoke(armor);
    }



}
