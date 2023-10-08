using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetoryUI : Inventory
{
    [SerializeField] private BodyMenu _body;
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ContentPanel _prefabPanel;
    [SerializeField] private DescriptionHolder _description;

    private ItemType _curretItem;
    private ContentPanel _select;
    private List<ContentPanel> _items = new List<ContentPanel>();

    private void Start()
    {
        ShowItem(ItemType.Armor);
    }

    public override void ShowItem(ItemType type)
    {
        if (_curretItem != type)
        {
            _curretItem = type;
            foreach (var item in _items)
            {
                item.gameObject.SetActive(item.Content.Type == type);
            }
        }
    }

    public override void AddItem(Item item)
    {
        var panel = Instantiate(_prefabPanel.gameObject, _contentHolder).
            GetComponent<ContentPanel>();
        item.BindPanel(panel);
        panel.OnSelect += Select;
        panel.OnDeselect += Deselect;
        _items.Add(panel);
    }



    private void Select(InvetoryPanel panel)
    {
        if (_select)
            _select.Deselect();
        _select = panel as ContentPanel;
        _body.SetArmor(_select.Content);
        _description.Show(_select.Content);
    }


    private void Deselect(InvetoryPanel panel)
    {
        _select.Deselect();
        _description.Hide();
        _select = null;
        _body.SetArmor(null);
    }


}
