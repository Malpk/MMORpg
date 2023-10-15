using UnityEngine;
using System.Collections.Generic;

public class GameSwitcher : MonoBehaviour
{
    [Min(0)]
    [SerializeField] private int _gloryWin;
    [Min(0)]
    [SerializeField] private int _gloryLose;
    [SerializeField] private Vector2Int _rewardRange;
    [Header("Reference")]
    [SerializeField] private List<Enemy> _enemys;
    [SerializeField] private Player _player;
    [SerializeField] private EndMenu _endMenu;
    [SerializeField] private LootHolder _lootSpawner;
    [SerializeField] private BaseDataSaver _saver;
    [SerializeField] private PvpControlelr _controller;

    private void OnEnable()
    {
        foreach (var enemy in _enemys)
        {
            enemy.Body.OnDead += DeadEnemy;
        }
        _player.Body.OnDead += DeadPlayer;
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemys)
        {
            enemy.Body.OnDead -= DeadEnemy;
        }
        _player.Body.OnDead -= DeadPlayer;
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


    private void DeadPlayer()
    {
        _player.Glory.RemoveGlory(_gloryLose);
        CompliteGame(-_gloryLose);
    }

    private void DeadEnemy()
    {
        if (!GetActiveEnemy())
        {
            var reward = 0;
            foreach (var enemy in _enemys)
            {
                _lootSpawner.AddLoot(enemy.Loot);
                reward += enemy.Level * Random.Range(_rewardRange.x, _rewardRange.y);
            }
            _player.Glory.AddGlory(_gloryWin);
            _player.Wallet.TakeMoney(reward);
            CompliteGame(_gloryWin, reward);
        }
    }
    private void CompliteGame(int glory, int reward = 0)
    {
        _controller.enabled = false;
        _endMenu.ShowMenu(glory, reward);
        _saver.Save();
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
