using UnityEngine;

public class ItemPanel : InvetoryPanel
{
    [SerializeField] private SkillItem _content;
    [SerializeField] private TextUI _mana;
    [SerializeField] private TextUI _description;
    [SerializeField] private TextUI _setSkillScore;

    public SkillItem Content => _content;

    public void SetContent(SkillItem item)
    {
        _content = item;
    }

    public void SetSkillSkore(int score)
    {
        _setSkillScore.SetText($"{score}/{_content.SkillScore}");
    }

    public void SetDescription(string description)
    {
        _description.SetText(description);
    }

    public void SetMana(int mana)
    {
        _mana.gameObject.SetActive(mana > 0);
        _mana.SetText(mana.ToString());
    }
}
