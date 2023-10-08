using UnityEngine;

public class Potion : SkillItem
{
    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Player player)
    {
        Debug.Log("Use");
    }

    public void BindDescription(PotionDescription panel)
    {
        panel.SetData(data.Name, data.Icon, data.Cost);
    }
}
