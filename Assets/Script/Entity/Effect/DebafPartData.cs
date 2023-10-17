using UnityEngine;

[CreateAssetMenu(menuName = "Effect/PartDebafData")]
public class DebafPartData : ScriptableObject
{
    [SerializeField] private int _level;
    [SerializeField] private string _name;
    [SerializeField] private Vector2 _debafRange;
    [SerializeField] private DamageType _damage;

    public int Level => _level;
    public string Name => _name;

    private void OnValidate()
    {
        _debafRange.x = _debafRange.x < 0 ? 0 : _debafRange.x;
        _debafRange.y = _debafRange.y < 0 ? 0 : _debafRange.y;
    }

    public float GetDebaf()
    {
        return Random.Range(_debafRange.x, _debafRange.y);
    }

}
