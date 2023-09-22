using UnityEngine;

public class BodyPanel : MonoBehaviour
{
    [SerializeField] private TextUI _armorProtect;

    public void SetArmor(int armor)
    {
        _armorProtect.SetText(armor.ToString());
    }
}
