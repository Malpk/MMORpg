using UnityEngine;

public class ItemHub : MonoBehaviour
{
    [SerializeField] private Item[] _items;

    public Item[] Items => _items;

    public static ItemHub Instance { get; private set; }


    private void Awake()
    {
        if (Instance)
        {
            Debug.LogError("Destroy instance ItemHub");
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public static Item GetItem(int id)
    {
        foreach (var item in Instance.Items)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }

    public static T GetItem<T>(int id) where T : Item
    {
        return GetItem(id) as T;
    }
}
