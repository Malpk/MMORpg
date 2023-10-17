using UnityEngine;

public abstract class DebafPart : MonoBehaviour
{
    [SerializeField] private DebafDataHolder _debafHolder;
    
    private string _stateName;
    private DebafPartData _debafActive;

    public event System.Action<int> OnSetState;

    public abstract PartType Part { get; }

    public string StateName => _stateName;

    protected abstract Stats AddDebaf(Stats stats, DebafPartData data);

    public Stats AddDebaf(Stats stats, Attack attack)
    {
        var debaf = _debafHolder.GetDebaf();
        SetDebaf(debaf, attack);
        return AddDebaf(stats, debaf);
    }

    private void SetDebaf(DebafPartData debaf, Attack attacl)
    {
        var debafLevel = _debafActive ? _debafActive.Level : 0;
        if (debaf.Level > debafLevel)
        {
            _debafActive = debaf;
            var state = _debafHolder.GetState(_debafActive.Level, Part);
            _stateName = state.GetName(attacl.DamageType);
        }
    }
}
