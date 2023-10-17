using UnityEngine;

public abstract class DebafPart : MonoBehaviour
{
    [SerializeField] private DebafDataHolder _debafHolder;
    [SerializeField] private BodyPartState[] _states;

    private DebafPartData _debafActive;
    private BodyPartState _curretState;

    public abstract PartType Part { get; }

    public BodyPartState Data => _curretState;

    private void Awake()
    {
        SetState(PartState.Idle);
    }

    protected abstract Stats AddDebaf(Stats stats, DebafPartData data);

    public void SetState(PartState target)
    {
        foreach (var data in _states)
        {
            if (data.State == target)
            {
                _curretState = data;
                return;
            }
        }
    }
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
            SetStates(debaf.Level, attacl.DamageType);
        }
    }

    private bool SetStates(int level, DamageType damage)
    {
        foreach (var data in _states)
        {
            if(data.Level == level && data.Damage == damage)
            {
                _curretState = data;
                return true;
            }
        }
        return false;
    }
}
