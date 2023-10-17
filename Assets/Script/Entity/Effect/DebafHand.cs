public class DebafHand : DebafPart
{
    public override PartType Part => PartType.Hands;

    protected override Stats AddDebaf(Stats stats, DebafPartData data)
    {
        var debaf = data.GetDebaf();
        stats.Dexterity -= (int)(stats.Dexterity * debaf);
        stats.Survival -= (int)(stats.Survival * debaf);
        return stats;
    }
}
