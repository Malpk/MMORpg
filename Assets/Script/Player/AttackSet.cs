using UnityEngine;

public class AttackSet : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private MapHolder _map;
    [SerializeField] private AttackMenu _attackMenu;

    private Enemy _enemy;

    private void OnEnable()
    {
        _map.OnActive += OnPointEnter;
        _map.OnExit += OnPointExit;
        _attackMenu.OnAttack += Attack;
        _attackMenu.OnProtect += Protect;
    }

    private void OnDisable()
    {
        _map.OnActive -= OnPointEnter;
        _map.OnExit -= OnPointExit;
        _attackMenu.OnAttack -= Attack;
        _attackMenu.OnProtect -= Protect;
    }

    private void Protect(PartType[] part)
    {
        _player.Skip();
    }

    public void Attack(PartType[] parts)
    {
        foreach (var part in parts)
        {
            _enemy.Body.TakeDamage(_player.Attack, part);
        }
        _player.Skip();
    }

    #region Point
    private void OnPointEnter(MapPoint point)
    {
        if (point.Content)
        {
            if (point.Content.TryGetComponent(out Enemy enemy))
            {
                _attackMenu.BindMenu(enemy);
                _enemy = enemy;
            }
        }
    }

    private void OnPointExit(MapPoint point)
    {
        _attackMenu.BindMenu(null);
    }
    #endregion
}
