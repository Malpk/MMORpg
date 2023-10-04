using System.Collections.Generic;
using UnityEngine;

public class InvetoryUI : MonoBehaviour
{
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ItemPanel _itemPanel;
    [SerializeField] private ItemDescriptionPanel _decription;

    private ItemType _curretItem;

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

    public void ShowItem(ItemType type)
    {
        if (_curretItem != type)
        {
            _curretItem = type;
            foreach (var item in _items)
            {
                item.gameObject.SetActive(item.Content.Type == type);
            }
            _decription.SetMode(false);
        }
    }

    public void SetSkillScore(PvpSkillScore skill)
    {
        foreach (var item in _items)
        {
            item.SetSkillSkore(skill.Score);
            if (item.Content.SkillScore <= skill.Score)
            {
                item.transform.SetAsFirstSibling();
            }
            else
            {
                item.transform.SetAsLastSibling();
            }
        }
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
