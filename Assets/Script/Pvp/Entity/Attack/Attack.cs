public class Attack
{
    public readonly int Damage;
    public readonly DamageType DamageType;
    public readonly int Power;
    public readonly Entity Attaker;

    public Attack(Entity attaker, int damage)
    {
        Damage = damage;
        DamageType = attaker.Hands.DamageType;
        Power = attaker.Stats.Stats.Luck + attaker.Stats.Stats.Dexterity;
        Attaker = attaker;
    }
}
