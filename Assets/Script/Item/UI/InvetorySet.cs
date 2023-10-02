using System.Collections.Generic;
using UnityEngine;

public class InvetorySet : MonoBehaviour
{
    [SerializeField] private InvetoryMenu _menu;
    [SerializeField] private List<Item> _contents;

    private void Awake()
    {
        foreach (var item in _contents)
        {
            AddItem(item);
        }
    }

    public void AddItem(Item item)
    {
        _menu.AddItem(item);
    }
}
