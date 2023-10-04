using UnityEngine;

public class PvpInvetoryMenu : UIMenu
{
    [SerializeField] private InvetoryUI _invetory;

    public void ShowPaper()
    {
        _invetory.ShowItem(ItemType.Paper);
    }

    public void ShowPolution()
    {
        _invetory.ShowItem(ItemType.Polution);
    }
}
