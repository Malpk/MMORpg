using UnityEngine;

[CreateAssetMenu(menuName ="Entity:")]
public class RadioItem
{
    [SerializeField] private string _name;

    public string Name => _name;
}
