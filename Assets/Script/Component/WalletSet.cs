using UnityEngine;

public class WalletSet : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private TextUI[] _moneyUI;

    public int Money => _money;

    private void OnValidate()
    {
        UpdateUI();
    }

    public void SetMoney(int money)
    {
        _money = money;
        UpdateUI();
    }

    public void TakeMoney(int money)
    {
        SetMoney(_money + money);
        UpdateUI();
    }

    public bool GiveMoney(int money)
    {
        if (_money - money >= 0)
        {
            SetMoney(_money - money);
            UpdateUI();
            return true;
        }
        return false;
    }

    private void UpdateUI()
    {
        foreach (var text in _moneyUI)
        {
            text?.SetText(_money.ToString());
        }
    }
}
