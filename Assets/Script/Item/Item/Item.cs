using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _level;
    [SerializeField] protected ItemData data;

    public int ID => _id;
    public int Level => _level;
    public Sprite Icon => data.Icon;
    public ItemType Type => data.Type;

    public abstract void Pick();

    public abstract void Use(Player player);

    #region Bind

    public virtual void BindPanel(ContentPanel panel)
    {
        panel.SetContent(this);
        panel.Preview(data.Name, data.Icon);
        panel.SetData(data.Cost, 0);
    }

    public virtual void BindDescription(ItemDescriptionPanel panel)
    {
        panel.Preview(data.Name, data.Icon);
        panel.SetDescription(data.Description);
    }

    #endregion
}
