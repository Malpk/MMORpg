using System.Collections.Generic;
using UnityEngine;

public class InvetorySet : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private WalletSet _playerWallet;
    [SerializeField] private Inventory _menu;
    [SerializeField] private List<Item> _contents;

    private void Awake()
    {
        foreach (var item in _contents)
        {
            AddItem(item);
        }
    }

    private void OnEnable()
    {
        _menu.OnUse += UseItem;
    }

    private void OnDisable()
    {
        _menu.OnUse -= UseItem;
    }

    public void Sell()
    {
        if (_menu.SelectItem)
        {
            var item = _menu.SelectItem;
            _menu.RemoveItem(item);
            _contents.Remove(item);
            _playerWallet.TakeMoney(item.Data.Cost);
        }
    }

    public void AddItem(Item item)
    {
        _menu.AddItem(item);
    }

    public void RemoveItem(Item item)
    {
        _menu.RemoveItem(item);
    }

    private void UseItem(Item item)
    {
        item.Use(_player);
    }
}
