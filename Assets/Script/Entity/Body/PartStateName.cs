using UnityEngine;

[System.Serializable]
public class PartStateName
{
    [SerializeField] private string _name;
    [SerializeField] private DamageType _damage;

    public string Name => _name;
    public DamageType Damage => _damage;

}
