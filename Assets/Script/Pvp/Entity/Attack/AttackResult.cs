public class AttackResult
{
    public readonly int Damage;
    public readonly AttackType Result;
    public readonly Entity Attaker;

    public AttackResult()
    {
        Damage = 0;
        Attaker = null;
        Result = AttackType.None;
    }

    public AttackResult(Entity attaker ,AttackType attack, int damage = 0)
    {
        Damage = damage;
        Result = attack;
        Attaker = attaker;
    }
}
