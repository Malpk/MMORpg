using UnityEngine;

public class Potion : SkillItem
{
    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Entity player)
    {
        Debug.Log("use");
    }
    public override void Drop()
    {
        Debug.Log("drop");
    }

    public void BindDescription(PotionDescription panel)
    {
        panel.SetData(data.Name, data.Icon, data.Cost);
    }
}
