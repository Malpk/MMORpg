using UnityEngine;

public class InterfaceSwitcher : MonoBehaviour
{
    [SerializeField] private UIMenu _openMenu;
    [SerializeField] private UIMenu[] _menu;


    public MenuType OpenMenu { get; private set; }

    public void SwitchMenu(MenuType type)
    {
        if (_openMenu)
            _openMenu.Hide();
        OpenMenu = type;
        if (type != MenuType.None)
        {
            _openMenu = GetMenu(type);
            _openMenu.Show();
        }
    }

    private UIMenu GetMenu(MenuType type)
    {
        foreach (var menu in _menu)
        {
            if (menu.Type == type)
            {
                return menu;
            }
        }
        return null;
    }
}
