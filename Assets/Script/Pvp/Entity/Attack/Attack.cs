public class Attack
{
    public readonly int Damage;
    public readonly int Luck;
    public readonly int Dexterity;
    public readonly Entity Attaker;

    public Attack(Entity attaker, int damage)
    {
        Damage = damage;
        Luck = attaker.Stats.Stats.Luck;
        Dexterity = attaker.Stats.Stats.Dexterity;
        Attaker = attaker;
    }
}
