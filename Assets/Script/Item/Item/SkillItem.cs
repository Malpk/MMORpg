
using UnityEngine;

public abstract class SkillItem : Item
{
    [Min(1)]
    [SerializeField] private int _skillScore;

    public int SkillScore => _skillScore;

    public virtual void BindPanel(ItemPanel panel)
    {
        panel.SetContent(this);
        panel.Preview(data.Name, data.Icon);
        panel.SetDescription(data.Description);
    }
}
