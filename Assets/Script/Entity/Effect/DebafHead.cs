public class DebafHead : DebafPart
{
    public override PartType Part => PartType.Head;

    protected override Stats AddDebaf(Stats stats, DebafPartData data)
    {
        var debaf = stats.Intelligence * data.GetDebaf();
        stats.Intelligence -= (int)debaf;
        return stats;
    }
}
