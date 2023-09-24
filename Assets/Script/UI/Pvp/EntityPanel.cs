using UnityEngine;
using TMPro;

public class EntityPanel : MonoBehaviour
{
    [SerializeField] private Entity _entity;
    [SerializeField] private TextMeshProUGUI _hard;
    [SerializeField] private TextUI _health;
    [SerializeField] private TextUI _damage;
    [SerializeField] private TextUI _level;


    private void OnValidate()
    {
        if (_entity)
        {
            _health?.SetText(_entity.Health.ToString());
            _damage?.SetText(_entity.Attack.ToString());
            _level?.SetText(_entity.Level.ToString());
        }
    }

    public void SetEntity(Entity entity)
    {
        _health.SetText(entity.Health.ToString());
        _damage.SetText(entity.Attack.ToString());
        _level.SetText(entity.Level.ToString());
    }
}
