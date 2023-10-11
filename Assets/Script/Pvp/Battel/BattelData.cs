using UnityEngine;

[CreateAssetMenu(menuName = "Pvp/Battel")]
public class BattelData : ScriptableObject
{
    [SerializeField] private string _locationName;
    [SerializeField] private Enemy[] _entity;

    public string Location => _locationName;
    public Enemy[] Entities => _entity;
}
