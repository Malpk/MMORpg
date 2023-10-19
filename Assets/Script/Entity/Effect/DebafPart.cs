using UnityEngine;

public abstract class DebafPart : MonoBehaviour
{
    [SerializeField] private string _stateName;
    [SerializeField] private DamageType _damage;

    [SerializeField] protected DebafPartData debafActive;
    [Header("Reference")]
    [SerializeField] private DebafDataHolder _debafHolder;

    [SerializeField] protected float debaf;


    public event System.Action OnUpdateState;

    public abstract PartType Part { get; }

    public int Level => debafActive ? debafActive.Level : 0;
    public string StateName => _stateName;

    public abstract Stats AddDebaf(Stats stats, Stats baseStats);

    #region Save / Load
    public SavePartState Save()
    {
        var save = new SavePartState();
        if (debafActive)
        {
            save.Level = debafActive.Level;
            save.Debaf = debaf;
            save.Damage = _damage;
        }
        return save;
    }

    public void Load(SavePartState save)
    {
        if (save.Level > 0)
        {
            debafActive = _debafHolder.GetDebaf(save.Level);
            debaf = save.Debaf;
            SetState(save.Damage);
        }
    }
    #endregion

    public void TakeHeal(int level)
    {
        if (level >= Level)
        {
            debafActive = null;
            _stateName = "";
            _damage = DamageType.None;
            debaf = 0;
            OnUpdateState?.Invoke();
        }
    }

    public void TakeDamage(Attack attack)
    {
        var debaf = _debafHolder.GetDebaf();
        SetDebaf(debaf, attack);
        OnUpdateState?.Invoke();
    }

    private void SetDebaf(DebafPartData data, Attack attacl)
    {
        var debafLevel = debafActive ? debafActive.Level : 0;
        if (data.Level > debafLevel)
        {
            debafActive = data;
            debaf = debafActive.GetDebaf();
            SetState(attacl.DamageType);
        }
    }

    private void SetState(DamageType damage)
    {
        var state = _debafHolder.GetState(debafActive.Level, Part);
        _damage = damage;
        _stateName = state.GetName(damage);
    }
}
