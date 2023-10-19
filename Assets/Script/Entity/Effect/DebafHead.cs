using UnityEngine;

public class DebafHead : DebafPart
{
    public override PartType Part => PartType.Head;

    public override Stats AddDebaf(Stats stats, Stats baseStas)
    {
        if (debafActive)
        {
            stats.Intelligence -= Mathf.RoundToInt(baseStas.Intelligence * debaf);
        }
        return stats;
    }
}
