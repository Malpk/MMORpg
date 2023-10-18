public class DebafLeg : DebafPart
{
    public override PartType Part => PartType.Leg;

    public override Stats AddDebaf(Stats stats)
    {
        if (debafActive)
        {
            var debaf = debafActive.GetDebaf();
            stats.Dexterity -= (int)(stats.Dexterity * debaf);
            stats.Survival -= (int)(stats.Survival * debaf);
        }
        return stats;
    }
}
