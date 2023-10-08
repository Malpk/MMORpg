using UnityEngine;
using System.Collections.Generic;

public class ArmorSet : MonoBehaviour
{
    [SerializeField] private EntityBody _body;

    private List<Armor> _list = new List<Armor>();

    public void AddArmor(Armor armor)
    {
        _body.AddArmor(armor);
    }
}
