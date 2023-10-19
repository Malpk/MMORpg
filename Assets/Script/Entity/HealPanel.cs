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
        UpdateButton();
        _heal.OnReady += UpdateButton;
    }

    private void OnDisable()
    {
        _heal.OnReady -= UpdateButton;
    }

    public void Heal()
    {
        if (_player.Wallet.GiveMoney(_healPrice))
        {
            _heal.Heal();
            UpdateButton();
        }
    }

    private void UpdateButton()
    {
        _healButton.interactable = _player.Wallet.Money >= _healPrice;
        _healButton.interactable = _healButton.interactable && _heal.IsReady;
    }
}
