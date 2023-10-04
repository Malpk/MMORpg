using UnityEngine;

public class Polution : Item
{
    public override void Pick()
    {
        gameObject.SetActive(false);
    }

    public override void Use(Player player)
    {
        
    }
}
