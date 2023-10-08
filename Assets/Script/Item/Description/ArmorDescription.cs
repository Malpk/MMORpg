using UnityEngine;

public class ArmorDescription : BaseDescription
{
    [SerializeField] private TextUI _level;
    [SerializeField] private TextUI _armorSet; 
    [SerializeField] private TextUI _protect;
    [SerializeField] private TextUI _edurance;
    [SerializeField] private TextUI _armorClass;

    public void SetArmor(int level, int edurance, ArmorData armor)
    {
        _level.SetText(level.ToString());
        _protect.SetText(armor.Protect.ToString());
        _edurance.SetText(edurance.ToString());
        _armorSet.SetText(armor.Type.Name);
        _armorClass.SetText(armor.Class.Name);
    }
}
