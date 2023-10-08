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

    private List<ItemPanel> _panels = new List<ItemPanel>();
    private List<ItemPanel> _pool = new List<ItemPanel>();

    public override Item SelectItem => _select.Content;

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
            var panel = CreatePanel();
            item.BindPanel(panel);
            panel.OnSelect += Select;
            panel.OnDeselect += Deselect;
            _panels.Add(panel);
        }
    }

    public override void RemoveItem(Item item)
    {
        var panel = GetPanel(item);
        if (panel)
        {
            _pool.Add(panel);
            _panels.Remove(panel);
            panel.OnSelect -= Select;
            panel.OnDeselect -= Deselect;
            panel.gameObject.SetActive(false);
        }
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
            _decription.SetMode(false);
        }
    }

    public void SetSkillScore(PvpSkillScore skill)
    {
        foreach (var item in _panels)
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


    private ItemPanel GetPanel(Item target)
    {
        foreach (var panel in _panels)
        {
            if (panel.Content.ID == target.ID)
            {
                return panel;
            }
        }
        return null;
    }

    private ItemPanel CreatePanel()
    {
        if (_pool.Count > 0)
        {
            var panel = _pool[0];
            _pool.Remove(panel);
            panel.gameObject.SetActive(true);
        }
        return Instantiate(_itemPanel.gameObject, _contentHolder).
             GetComponent<ItemPanel>();
    }
}
