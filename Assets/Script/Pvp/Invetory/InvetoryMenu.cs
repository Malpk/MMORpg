using UnityEngine;

public class InvetoryMenu : UIMenu
{
    [SerializeField] private Inventory _invetory;

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
