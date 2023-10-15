using UnityEngine;
using System.Collections.Generic;

public class LootHolder : MonoBehaviour
{
    [SerializeField] private Vector2 _probilityRange;
    [Header("Reference")]
    [SerializeField] private InvetorySet _playerInvetory;

    public void AddLoot(LootSet loot)
    {
        var list = GetArmor(loot.Parts);
        list.AddRange(GetItem(loot.Items));
        foreach (var item in list)
        {
            _playerInvetory.AddItem(item);
        }
    }

    private List<Item> GetItem(Item[] items)
    {
        var list = new List<Item>();
        foreach (var item in items)
        {
            var probility = Random.Range(0, 1f);
            var targetProbilty = Random.Range(_probilityRange.x, _probilityRange.y);
            if (probility < targetProbilty)
            {
                list.Add(item);
            }
        }
        return list;
    }

    private List<Item> GetArmor(PartBody[] parts)
    {
        var list = new List<Item>();
        foreach (var item in parts)
        {
            if (item.Armor)
            {
                var probility = Random.Range(0, 1f);
                var targetProbilty = Random.Range(_probilityRange.x, _probilityRange.y);
                if (probility < targetProbilty)
                {
                    list.Add(item.Armor);
                }
            }
        }
        return list;
    }

}
