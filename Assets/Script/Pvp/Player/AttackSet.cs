using UnityEngine;

public class AttackSet : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private int _attackDistance;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private PvpSkills _skills;
    [SerializeField] private MapHolder _map;
    [SerializeField] private AttackMenu _attackMenu;

    private Enemy _enemy;

    private void Awake()
    {
        _attackMenu.OnAttack += Attack;
        _attackMenu.OnProtect += Protect;
    }

    private void OnEnable()
    {
        _map.OnActive += OnPointEnter;
        _map.OnExit += OnPointExit;

    }

    private void OnDisable()
    {
        _map.OnActive -= OnPointEnter;
        _map.OnExit -= OnPointExit;
    }

    private void OnDestroy()
    {
        _attackMenu.OnAttack -= Attack;
        _attackMenu.OnProtect -= Protect;
    }

    private void Protect(PartType[] parts)
    {
        foreach (var part in parts)
        {
            _player.Body.SetProtect(part, true);
        }
        _player.Stop();
    }

    public void Attack(PartType[] parts)
    {
        foreach (var part in parts)
        {
            var result = _player.Attack(_enemy, part);
            switch (result.Result)
            {
                case AttackType.Full:
                    _skills.AddScore(PvpScoreType.Attack);
                    break;
            }
        }
        _player.Stop();
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
