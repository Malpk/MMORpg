using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private ItemType _type;
    [TextArea(3,6)]
    [SerializeField] private string _description;

    public int ID => _id;
    public ItemType Type => _type;

    public abstract void Pick();

    public abstract void Use(Player player);

    #region Bind
    public virtual void BindPanel(ItemPanel panel)
    {
        panel.SetContent(this);
        panel.Preview(_name, _icon);
        panel.SetDescription(_description);
    }

    public virtual void BindDescription(ItemDescriptionPanel panel)
    {
        panel.Preview(_name, _icon);
        panel.SetDescription(_description);
    }
    #endregion
}
