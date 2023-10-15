using UnityEngine;
using UnityEngine.UI;

public class BodyMenu : MonoBehaviour
{
    [SerializeField] private Player _palyer;
    [SerializeField] private Button _useArmor;
    [SerializeField] private Button _unuseArmor;

    private Item _select;

    public void SetBodyItem(Item item)
    {
        if (item is Armor armor)
        {
            _select = armor;
            var contain = _palyer.Body.CheakContaintArmor(armor.Part);
            _useArmor.gameObject.SetActive(!contain);
            _unuseArmor.gameObject.SetActive(contain);
        }
        else if(item is Weapon weapon)
        {
            _select = weapon;
            var contain = _palyer.Hands.Weapon == weapon;
            _useArmor.gameObject.SetActive(!contain);
            _unuseArmor.gameObject.SetActive(contain);
        }
        else
        {
            _select = null;
            _useArmor.gameObject.SetActive(false);
            _unuseArmor.gameObject.SetActive(false);
        }
    }

    public void RemoveArmor(Item item)
    {
        if (item is Armor armor)
        {
            _palyer.Body.RemoveArmor(armor);
        }
    }

    public void AddItem()
    {
        _useArmor.gameObject.SetActive(false);
        _unuseArmor.gameObject.SetActive(true);
        _select.Use(_palyer);
    }

    public void RemoveItem()
    {
        _useArmor.gameObject.SetActive(true);
        _unuseArmor.gameObject.SetActive(false);
        _select.Drop();
    }

}
