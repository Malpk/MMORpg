using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerInvetoryUI : Inventory
{
    [SerializeField] private Button _sellButton;
    [SerializeField] private BodyMenu _body;
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ContentPanel _prefabPanel;
    [SerializeField] private DescriptionHolder _description;

    private ItemType _curretItem;
    private ContentPanel _select;
    private List<ContentPanel> _panels = new List<ContentPanel>();
    private List<ContentPanel> _pool = new List<ContentPanel>();

    public override Item SelectItem => _select ? _select.Content : null;

    private void Start()
    {
        _sellButton.interactable = false;
        ShowItem(ItemType.Armor);
    }

    public override void ShowItem(ItemType type)
    {
        if (_curretItem != type)
        {
            _curretItem = type;
            foreach (var item in _panels)
            {
                item.gameObject.SetActive(item.Content.Type == type);
            }
        }
    }

    public override void AddItem(Item item)
    {
        var panel = CreatePanel();
        item.BindPanel(panel);
        panel.OnSelect += Select;
        panel.OnDeselect += Deselect;
        _panels.Add(panel);
    }

    public override void RemoveItem(Item item)
    {
        if (_select)
        {
            _pool.Add(_select);
            _panels.Remove(_select);
            _select.OnSelect -= Select;
            _select.OnDeselect -= Deselect;
            _select.gameObject.SetActive(false);
            _select.Content.Drop();
            Deselect(_select);
        }
    }

    private void Select(InvetoryPanel panel)
    {
        if (_select)
            _select.Deselect();
        _select = panel as ContentPanel;
        _body.SetBodyItem(_select.Content);
        _description.Show(_select.Content);
        _sellButton.interactable = _select;
    }


    private void Deselect(InvetoryPanel panel)
    {
        _select.Deselect();
        _description.Hide();
        _select = null;
        _body.SetBodyItem(null);
        _sellButton.interactable = _select;
    }

    private ContentPanel CreatePanel()
    {
        if (_pool.Count > 0)
        {
            var panel = _pool[0];
            _pool.Remove(panel);
            panel.gameObject.SetActive(true);
        }
        return Instantiate(_prefabPanel.gameObject, _contentHolder).
             GetComponent<ContentPanel>();
    }
}
