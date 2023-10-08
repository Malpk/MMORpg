using UnityEngine;

[System.Serializable]
public struct ItemData
{
    public string Name;
    public int Cost;
    public Sprite Icon;
    public ItemType Type;
    [TextArea(3, 6)]
    public string Description;

}
