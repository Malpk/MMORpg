using UnityEngine;

public abstract class Inventory : MonoBehaviour
{
    public event System.Action<Item> OnUse;

    public abstract void AddItem(Item item);

    public abstract void ShowItem(ItemType type);

    protected void UseItemEvent(Item item)
    {
        OnUse?.Invoke(item);
    }
}
