using UnityEngine;

[CreateAssetMenu(menuName ="Item/ArmorClass")]
public class ArmorClass : ScriptableObject
{
    [SerializeField] private string _nameClass;

    public string Name => _nameClass;
}
