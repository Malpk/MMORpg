using UnityEngine;

public class DebafHand : DebafPart
{
    public override PartType Part => PartType.Hands;

    public override Stats AddDebaf(Stats stats, Stats baseStas)
    {
        if (debafActive)
        {
            stats.Dexterity -= Mathf.RoundToInt(baseStas.Dexterity * debaf);
            stats.Survival -= Mathf.RoundToInt(baseStas.Survival * debaf);
        }
        return stats;
    }
}
