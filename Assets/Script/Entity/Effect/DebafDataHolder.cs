using UnityEngine;

public class DebafDataHolder : MonoBehaviour
{
    [SerializeField] private PartState[] _states;
    [SerializeField] private DebafPartData[] _debafs;

    public DebafPartData GetDebaf()
    {
        var debaf = _debafs[Random.Range(0, _debafs.Length)];
        return debaf;
    }

    public DebafPartData GetDebaf(int level)
    {
        foreach (var debaf in _debafs)
        {
            if (debaf.Level == level)
                return debaf;
        }
        return null;
    }

    public PartState GetState(int level, PartType part)
    {
        var states = GetStates(level, part);
        if (!states)
            states = GetStates(level);
        return states;
    }

    private PartState GetStates(int level, PartType part = PartType.None)
    {
        foreach (var state in _states)
        {
            if (state.Level == level)
            {
                if (state.Part == part)
                    return state;
            }
        }
        return null;
    }
}
