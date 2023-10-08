using UnityEngine;

public class Armor : Item
{
    [SerializeField] private ArmorData _armorInfo;

    public int Protect => _armorInfo.Protect;
    public PartType Part => _armorInfo.Part;

    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Player player)
    {
        player.Body.AddArmor(this);
    }

    public void BindDescription(ArmorDescription description)
    {
        description.SetData(data.Name, data.Icon, data.Cost);
        description.SetArmor(Level, 2, _armorInfo);
    }

}
