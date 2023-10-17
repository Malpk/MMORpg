using UnityEngine;

[CreateAssetMenu(menuName ="Entity/BodyPartState")]
public class BodyPartState : ScriptableObject
{
    [SerializeField] private string _name;
    [Min(0)]
    [SerializeField] private int _level;
    [SerializeField] private PartState _state;
    [SerializeField] private DamageType _damage;

    public int Level => _level;
    public string StateName => _name;
    public PartState State => _state;
    public DamageType Damage => _damage;


}
