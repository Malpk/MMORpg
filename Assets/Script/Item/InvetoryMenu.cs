using System.Collections.Generic;
using UnityEngine;

public class InvetoryMenu : MonoBehaviour
{
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ItemPanel _itemPanel;
    [SerializeField] private ItemDescriptionPanel _decription;

    private ItemPanel _select;

    private List<ItemPanel> _items = new List<ItemPanel>();

    public void AddItem(Item item)
    {
        var panel = GetPanel();
        item.BindPanel(panel);
        panel.OnSelect += Select;
        panel.OnDeselect += Deselect;
        _items.Add(panel);
    }


    private ItemPanel GetPanel()
    {
        return Instantiate(_itemPanel.gameObject, _contentHolder).
            GetComponent<ItemPanel>();
    }

    public void Select(ItemPanel item)
    {
        if (_select)
            _select.Deselect();
        _select = item;
        _decription.SetMode(true);
        _select.Content.BindDescription(_decription);
    }

    private void Deselect(ItemPanel item)
    {
        _select = null;
        _decription.SetMode(false);
        _decription.Reload();
    }

}
