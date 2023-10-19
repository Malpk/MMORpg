using UnityEngine;

[CreateAssetMenu(menuName = "Effect/PartDebafData")]
public class DebafPartData : ScriptableObject
{
    [SerializeField] private int _level;
    [Range(0, 1f)]
    [SerializeField] private float _chanceMakeStatic;
    [SerializeField] private Vector2 _timeHeal;
    [SerializeField] private Vector2 _debafRange;

    public int Level => _level;
    public float ChanceMakeStatic => _chanceMakeStatic;

    private void OnValidate()
    {
        _debafRange.x = _debafRange.x < 0 ? 0 : _debafRange.x;
        _debafRange.y = _debafRange.y < 0 ? 0 : _debafRange.y;
    }
    public float GetTime()
    {
        return Random.Range(_timeHeal.x, _timeHeal.y);
    }
    public float GetDebaf()
    {
        return Random.Range(_debafRange.x, _debafRange.y);
    }

}
