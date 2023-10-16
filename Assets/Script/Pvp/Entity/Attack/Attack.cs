public class Attack
{
    public readonly int Damage;
    public readonly int Power;
    public readonly Entity Attaker;

    public Attack(Entity attaker, int damage)
    {
        Damage = damage;
        Power = attaker.Stats.Stats.Luck + attaker.Stats.Stats.Dexterity;
        Attaker = attaker;
    }
}
