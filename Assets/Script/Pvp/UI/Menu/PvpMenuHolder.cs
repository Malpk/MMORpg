using UnityEngine;

public class PvpMenuHolder : MonoBehaviour
{
    [SerializeField] private InterfaceSwitcher _interface;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuType.Attack == _interface.OpenMenu)
                ShowHud();
        }
    }

    public void ShowHud()
    {
        _interface.SwitchMenu(MenuType.HUD);
    }

    public void ShowAttack()
    {
        _interface.SwitchMenu(MenuType.Attack);
    }

    public void ShowEndMenu()
    {
        _interface.SwitchMenu(MenuType.EndMenu);
    }
}
