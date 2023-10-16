public class Attack
{
    public readonly int Damage;
    public readonly int Luck;
    public readonly int Dexterity;
    public readonly Entity Assaulter;


    public Attack(Entity assaulter, int damage)
    {
        Damage = damage;
        Luck = assaulter.Stats.Stats.Luck;
        Dexterity = assaulter.Stats.Stats.Dexterity;
        Assaulter = assaulter;
    }
}
