using UnityEngine;

public abstract class DebafPart : MonoBehaviour
{
    [SerializeField] private string _stateName;
    [SerializeField] private DebafDataHolder _debafHolder;
    
    protected DebafPartData debafActive;

    public event System.Action OnUpdateState;

    public abstract PartType Part { get; }

    public string StateName => _stateName;

    public abstract Stats AddDebaf(Stats stats);

    public void TakeDamage(Attack attack)
    {
        var debaf = _debafHolder.GetDebaf();
        SetDebaf(debaf, attack);
        OnUpdateState?.Invoke();
    }

    private void SetDebaf(DebafPartData debaf, Attack attacl)
    {
        var debafLevel = debafActive ? debafActive.Level : 0;
        if (debaf.Level > debafLevel)
        {
            debafActive = debaf;
            var state = _debafHolder.GetState(debafActive.Level, Part);
            _stateName = state.GetName(attacl.DamageType);
        }
    }
}
