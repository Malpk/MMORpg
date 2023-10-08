using UnityEngine;
using UnityEngine.UI;

public class BodyMenu : MonoBehaviour
{
    [SerializeField] private Player _palyer;
    [SerializeField] private Button _useArmor;
    [SerializeField] private Button _unuseArmor;

    private Armor _selectArmor;

    public void SetArmor(Item item)
    {
        if (item is Armor armor)
        {
            _selectArmor = armor;
            var contain = _palyer.Body.CheakContaintArmor(armor.Part);
            _useArmor.gameObject.SetActive(!contain);
            _unuseArmor.gameObject.SetActive(contain);
        }
        else
        {
            _selectArmor = null;
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

    public void AddArmor()
    {
        _useArmor.gameObject.SetActive(false);
        _unuseArmor.gameObject.SetActive(true);
        _palyer.Body.AddArmor(_selectArmor);
    }

    public void AddRemove()
    {
        _useArmor.gameObject.SetActive(true);
        _unuseArmor.gameObject.SetActive(false);
        _palyer.Body.RemoveArmor(_selectArmor.Part);
    }


}
