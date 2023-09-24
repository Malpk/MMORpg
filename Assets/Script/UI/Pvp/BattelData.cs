using UnityEngine;

[CreateAssetMenu(menuName = "Pvp/Battel")]
public class BattelData : ScriptableObject
{
    [SerializeField] private string _locationName;
    [SerializeField] private Entity[] _entity;

    public string Location => _locationName;
    public Entity[] Entities => _entity;
}
