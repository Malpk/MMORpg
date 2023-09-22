using UnityEngine;
using TMPro;

public class WalletSet : MonoBehaviour
{
    [SerializeField] private int _money;
    [SerializeField] private TextMeshProUGUI _text;

    public int Money => _money;

    public void SetMoney(int money)
    {
        _money = money;
        _text.SetText(_money.ToString());
    }

    public void TakeMoney(int money)
    {
        SetMoney(_money + money);
    }

    public bool GiveMoney(int money)
    {
        if (_money - money >= 0)
        {
            SetMoney(_money - money);
            return true;
        }
        return false;
    }
}
