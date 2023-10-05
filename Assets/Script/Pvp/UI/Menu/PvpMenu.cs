using UnityEngine;

public class PvpMenu : MonoBehaviour
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
}
