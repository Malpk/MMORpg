public class DebafBody : DebafPart
{
    public override PartType Part => PartType.Body;

    public override Stats AddDebaf(Stats stats)
    {
        if (debafActive)
        {
            var debaf = debafActive.GetDebaf();
            stats.Strenght -= (int)(stats.Strenght * debaf);
            stats.Body -= (int)(stats.Body * debaf);
        }
        return stats;
    }
}
