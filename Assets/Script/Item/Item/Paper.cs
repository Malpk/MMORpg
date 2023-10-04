using UnityEngine;

public class Paper : Item
{
    [Header("ItemSetting")]
    [Range(0, 100)]
    [SerializeField] private int _probility;
    [SerializeField] private int _mana; 
    [SerializeField] private string _useDescription;

    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Player player)
    {
        Debug.Log("use");
    }

    public override void BindDescription(ItemDescriptionPanel panel)
    {
        base.BindDescription(panel);
        panel.SetUseData(_probility, _useDescription, _mana);
    }
}
