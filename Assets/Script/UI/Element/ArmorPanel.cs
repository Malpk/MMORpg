using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmorPanel : MonoBehaviour
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Image _icon;
    [SerializeField] private Image _empty;
    [SerializeField] private TextUI _protect;

    private void OnValidate()
    {
        if (_empty)
            _empty.gameObject.SetActive(!_armor);
        if (_icon)
        {
            _icon.gameObject.SetActive(_armor);
            if(_armor)
                _icon.sprite = _armor.Icon;
        }
        _protect?.SetText(_armor ? _armor.Protect.ToString() : "0");
    }

    public void SetArmor(Armor armor)
    {
        if (armor)
        {
            _empty.gameObject.SetActive(false);
            _icon.gameObject.SetActive(true);
            _icon.sprite = armor.Icon;
            _protect.SetText(armor.Protect.ToString());
        }
        else
        {
            _empty.gameObject.SetActive(true);
            _icon.gameObject.SetActive(false);
        }
        _armor = armor;
    }
}
