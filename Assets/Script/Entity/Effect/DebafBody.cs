using UnityEngine;

public class DebafBody : DebafPart
{
    public override PartType Part => PartType.Body;

    public override Stats AddDebaf(Stats stats, Stats baseStats)
    {
        if (debafActive)
        {
            stats.Strenght -= Mathf.RoundToInt(baseStats.Strenght * debaf);
            stats.Body -= Mathf.RoundToInt(baseStats.Body * debaf);
        }
        return stats;
    }
}
