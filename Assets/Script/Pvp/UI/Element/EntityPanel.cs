using UnityEngine;
using TMPro;

public class EntityPanel : MonoBehaviour
{
    [SerializeField] private Enemy _entity;
    [SerializeField] private TextMeshProUGUI _hard;
    [SerializeField] private TextUI _health;
    [SerializeField] private TextUI _damage;
    [SerializeField] private TextUI _level;

    public Enemy Enemy => _entity;

    private void OnValidate()
    {
        SetEntity(_entity);
    }

    public void SetEntity(Enemy entity)
    {
        _entity = entity;
        if (entity)
        {
            _health?.SetText(entity.Body.Health.ToString());
            _damage?.SetText($"{entity.RangeAttack.x}-{entity.RangeAttack.y}");
            _level?.SetText(entity.Level.ToString());
        }
        else 
        {
            _health?.SetText("0");
            _damage?.SetText("0");
            _level?.SetText("0");
        }
    }
}
