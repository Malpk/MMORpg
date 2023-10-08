using UnityEngine;

public class Polution : SkillItem
{
    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Player player)
    {
        Debug.Log("Use");
    }
}
