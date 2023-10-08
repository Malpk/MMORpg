using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class InventorySkillMenu : Inventory
{
    [SerializeField] private Button _useButton;
    [SerializeField] private ItemPanel _itemPanel;
    [SerializeField] private Transform _contentHolder;
    [SerializeField] private ItemDescriptionPanel _decription;

    private ItemType _curretItem;

    private ItemPanel _select;
    private PvpSkillScore _skillScore;

    private List<ItemPanel> _items = new List<ItemPanel>();

    public void Use()
    {
        if (_skillScore.GiveSkore(_select.Content.SkillScore))
        {
            UseItemEvent(_select.Content);
            SetSkillScore(_skillScore);
            _useButton.interactable = _select.Content.SkillScore <= _skillScore.Score;
        }
    }

    public override void AddItem(Item content)
    {
        if (content is SkillItem item)
        {
            var panel = GetPanel();
            item.BindPanel(panel);
            panel.OnSelect += Select;
            panel.OnDeselect += Deselect;
            _items.Add(panel);
        }
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
        _skillScore = skill;
        if(_select)
            _useButton.interactable = _select.Content.SkillScore <= _skillScore.Score;
    }

    private ItemPanel GetPanel()
    {
        return Instantiate(_itemPanel.gameObject, _contentHolder).
            GetComponent<ItemPanel>();
    }

    public void Select(InvetoryPanel panel)
    {
        var item = panel as ItemPanel;
        if (_select)
            _select.Deselect();
        _select = item;
        _decription.SetMode(true);
        _select.Content.BindDescription(_decription);
        _useButton.interactable = _select.Content.SkillScore <= _skillScore.Score;
    }

    private void Deselect(InvetoryPanel item)
    {
        _select = null;
        _decription.SetMode(false);
        _decription.Reload();
    }

}