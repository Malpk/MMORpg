using System.Collections.Generic;
using UnityEngine;

public class PlayerInvetoryUI : Inventory
{
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ContentPanel _prefabPanel;
    [SerializeField] private ArmorDescription _armorDescription;
    [SerializeField] private PaperDescription _papaerDescription;

    private ItemType _curretItem;
    private ContentPanel _select;
    private GameObject _description;
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
        if (_select.Content is Armor armor)
        {
            ShowDescription(_armorDescription.gameObject);
            armor.BindDescription(_armorDescription);
        }
        else if (_select.Content is Paper paper)
        {
            ShowDescription(_papaerDescription.gameObject);
            paper.BindDescription(_papaerDescription);
        }
    }

    public void ShowDescription(GameObject description)
    {
        _description?.SetActive(false);
        _description = description;
        _description.SetActive(true);
    }

    private void Deselect(InvetoryPanel panel)
    {
        _select.Deselect();
        _select = null;
    }


}
