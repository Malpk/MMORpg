using System.Collections.Generic;
using UnityEngine;

public class InvetorySet : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private InvetoryUI _menu;
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
        
    }

    public void AddItem(Item item)
    {
        _menu.AddItem(item);
    }

    private void UseItem(Item item)
    {
        item.Use(_player);
    }
}
