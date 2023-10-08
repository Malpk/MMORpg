using UnityEngine;
using TMPro;

public class PlayerPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextUI _playerName;
    [SerializeField] private TextMeshProUGUI _playerRang;


    private void OnEnable()
    {
        LoadPlayer(_player.Data);
        _player.OnSetData += LoadPlayer;

    }

    private void OnDisable()
    {
        _player.OnSetData -= LoadPlayer;
    }

    public void LoadPlayer(EntityData data)
    {
        _playerName.SetText(data.Name);
    }
}
