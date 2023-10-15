using UnityEngine;

public class InvetoryMenu : UIMenu
{
    [SerializeField] private Inventory _invetory;

    public void ShowWeapon()
    {
        _invetory.ShowItem(ItemType.Weapon);
    }

    public void ShowPaper()
    {
        _invetory.ShowItem(ItemType.Paper);
    }

    public void ShowArmor()
    {
        _invetory.ShowItem(ItemType.Armor);
    }

    public void ShowPolution()
    {
        _invetory.ShowItem(ItemType.Polution);
    }
}
