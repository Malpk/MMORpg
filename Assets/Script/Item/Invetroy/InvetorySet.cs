using System.Collections.Generic;
using UnityEngine;

public class InvetorySet : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Inventory _menu;
    [SerializeField] private List<Item> _contents;

    private void Awake()
    {
        _contents.Clear();
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

    #region Save
    public SaveInvetory Save()
    {
        var save = new SaveInvetory();
        save.Items = new int[_contents.Count];
        for (int i = 0; i < save.Items.Length; i++)
        {
            save.Items[i] = _contents[i].ID;
        }
        return save;
    }

    public void Load(SaveInvetory save)
    {
        _contents.Clear();
        for (int i = 0; i < save.Items.Length; i++)
        {
            var item = ItemHub.GetItem(save.Items[i]);
            AddItem(item);
        }
    }
    #endregion

    #region Item
    public void Sell()
    {
        if (_menu.SelectItem)
        {
            var item = _menu.SelectItem;
            _menu.RemoveItem(item);
            _contents.Remove(item);
            _player.Wallet.TakeMoney(item.Data.Cost);
        }
    }

    public void AddItem(Item item)
    {
        _menu.AddItem(item);
        _contents.Add(item);
    }

    public void RemoveItem(Item item)
    {
        _menu.RemoveItem(item);
        _contents.Remove(item);
    }

    private void UseItem(Item item)
    {
        item.Use(_player);
    }
    #endregion
}
