using UnityEngine;

public class Armor : Item
{
    [SerializeField] private ArmorData _armorInfo;

    private Entity _user;

    public int Protect => _armorInfo.Protect;
    public PartType Part => _armorInfo.Part;

    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Entity player)
    {
        _user = player;
        player.Body.AddArmor(this);
    }

    public override void Drop()
    {
        _user.Body.RemoveArmor(Part);
    }

    public void BindDescription(ArmorDescription description)
    {
        description.SetData(data.Name, data.Icon, data.Cost);
        description.SetArmor(Level, 2, _armorInfo);
    }


}
