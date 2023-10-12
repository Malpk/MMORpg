using UnityEngine;

public class LootSet : MonoBehaviour
{
    [SerializeField] private EntityBody _body;
    [SerializeField] private Item[] _loot;

    public Item[] Items => _loot;
    public PartBody[] Parts => _body.Parts;
}
