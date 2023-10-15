[System.Serializable]
public class SaveWeaponSkill 
{
    public int WeaponId;
    public WeaponSkill Skill;

    public SaveWeaponSkill(int id)
    {
        WeaponId = id;
    }
}
