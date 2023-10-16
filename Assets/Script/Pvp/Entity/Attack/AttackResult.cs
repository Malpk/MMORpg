public class AttackResult
{
    public readonly int Damage;
    public readonly AttackType Result;

    public AttackResult()
    {
        Damage = 0;
        Result = AttackType.None;
    }

    public AttackResult(AttackType attack, int damage = 0)
    {
        Damage = damage;
        Result = attack;
    }
}
