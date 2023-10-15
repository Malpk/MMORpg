using UnityEngine;

public class Paper : SkillItem
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

    public override void Use(Entity player)
    {
        Debug.Log("use");
    }
    public override void Drop()
    {
        Debug.Log("drop");
    }

    public override void BindPanel(ItemPanel panel)
    {
        base.BindPanel(panel);
        panel.SetMana(_mana);
    }

    public override void BindDescription(ItemDescriptionPanel panel)
    {
        base.BindDescription(panel);
        panel.SetUseData(_probility, _useDescription, _mana);
    }
    public void BindDescription(PaperDescription panel)
    {
        panel.SetData(data.Name, data.Icon, data.Cost);
        panel.SetAddition(_mana);
    }


}
