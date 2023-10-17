using UnityEngine;

[CreateAssetMenu(menuName ="Effect/BodyPartState")]
public class PartState : ScriptableObject
{
    [Min(0)]
    [SerializeField] private int _level;
    [SerializeField] private PartType _part;
    [SerializeField] private PartStateName[] _partStats;

    public int Level => _level;
    public PartType Part => _part;

    public string GetName(DamageType damage)
    {
        foreach (var state in _partStats)
        {
            if (state.Damage == damage)
                return state.Name;
        }
        return _partStats[0].Name;
    }
}
