public class DebafHead : DebafPart
{
    public override PartType Part => PartType.Head;

    public override Stats AddDebaf(Stats stats)
    {
        if (debafActive)
        {
            var debaf = stats.Intelligence * debafActive.GetDebaf();
            stats.Intelligence -= (int)debaf;
        }
        return stats;
    }
}
