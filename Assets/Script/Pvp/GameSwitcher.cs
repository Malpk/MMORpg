using UnityEngine;
using System.Collections.Generic;

public class GameSwitcher : MonoBehaviour
{
    [SerializeField] private Vector2Int _reward;
    [Header("Reference")]
    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Player _player;
    [SerializeField] private EndMenu _endMenu;
    [SerializeField] private DataSaver _saver;
    [SerializeField] private PvpControlelr _controller;

    private void OnEnable()
    {
        foreach (var enemy in _enemys)
        {
            enemy.Body.OnDead += DeadEnemy;
        }
        _player.Body.OnDead += () => CompliteGame();
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemys)
        {
            enemy.Body.OnDead -= DeadEnemy;
        }
        _player.Body.OnDead -= () => CompliteGame();
    }

    public void AddEnemy(Enemy enemy)
    {
        if (!_enemys.Contains(enemy))
        {
            if (enabled)
                enemy.Body.OnDead += DeadEnemy;
            _enemys.Add(enemy);
        }
    }

    private void CompliteGame(int reward = 0)
    {
        _controller.enabled = false;
        _endMenu.ShowMenu(reward);
        _saver.Save();
    }

    private void DeadEnemy()
    {
        if (!GetActiveEnemy())
        {
            var reward = 0;
            foreach (var enemy in _enemys)
            {
                reward += enemy.Level * Random.Range(_reward.x, _reward.y);
            }
            _player.Wallet.TakeMoney(reward);
            CompliteGame(reward);
        }
    }

    private Enemy GetActiveEnemy()
    {
        foreach (var enemy in _enemys)
        {
            if (!enemy.Body.IsDead)
                return enemy;
        }
        return null;
    }
}
