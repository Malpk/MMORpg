using UnityEngine;

public class DebafBody : DebafPart
{
    public override PartType Part => PartType.Body;

    protected override Stats AddDebaf(Stats stats, DebafPartData data)
    {
        var debaf = data.GetDebaf();
        stats.Strenght -= (int)(stats.Strenght * debaf);
        stats.Body -= (int)(stats.Body * debaf);
        return stats;
    }
}
