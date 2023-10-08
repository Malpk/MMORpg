using UnityEngine;


[CreateAssetMenu(menuName = "Item/ArmorType")]
public class ArmorType : ScriptableObject
{
    [SerializeField] private string _name;

    public string Name => _name;
}
