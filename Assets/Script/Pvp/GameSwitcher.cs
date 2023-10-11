using UnityEngine;

public class GameSwitcher : MonoBehaviour
{
    [SerializeField] private Vector2Int _reward;
    [Header("Reference")]
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Player _player;
    [SerializeField] private EndMenu _endMenu;
    [SerializeField] private DataSaver _saver;
    [SerializeField] private PvpControlelr _controller;

    private void OnEnable()
    {
        _enemy.Body.OnDead += CompliteGame;
        _player.Body.OnDead += CompliteGame;
    }

    private void OnDisable()
    {
        _enemy.Body.OnDead -= CompliteGame;
        _player.Body.OnDead -= CompliteGame;
    }

    private void CompliteGame()
    {
        _controller.enabled = false;
        if (_player.Body.Health > 0)
        {
            var reward = _enemy.Level * Random.Range(_reward.x, _reward.y);
            _player.Wallet.TakeMoney(reward);
            _endMenu.ShowMenu(reward);

        }
        else 
        {
            _endMenu.ShowMenu();
        }
        _saver.Save();
    }
}
