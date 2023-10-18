using UnityEngine;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextUI _playerName;
    [SerializeField] private Field _health;
    [SerializeField] private TextMeshProUGUI _playerRang;


    private void OnEnable()
    {
        LoadPlayer();
        _player.OnLoad += LoadPlayer;
        _player.OnChangeHealth += (float value) => LoadPlayer();
    }

    private void OnDisable()
    {
        _player.OnLoad -= LoadPlayer;
        _player.OnChangeHealth -= (float value) => LoadPlayer();
    }

    public void LoadPlayer()
    {
        _playerName.SetText(_player.Data.Name);
        _health.SetValue(_player.HealthNormalize);
    }
}
