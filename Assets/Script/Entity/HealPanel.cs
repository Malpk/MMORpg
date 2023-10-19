using UnityEngine;
using UnityEngine.UI;

public class HealPanel : MonoBehaviour
{
    [SerializeField] private int _healPrice;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private HealSet _heal;
    [SerializeField] private Button _healButton;

    private void OnEnable()
    {
        _healButton.interactable = _player.Wallet.Money >= _healPrice;
    }

    public void Heal()
    {
        if (_player.Wallet.GiveMoney(_healPrice))
        {
            _heal.Heal();
            _healButton.interactable = _player.Wallet.Money >= _healPrice;
        }
    }
}
