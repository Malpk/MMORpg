using UnityEngine;

[CreateAssetMenu(menuName ="Entity/RaceStas")]
public class RaceStats : ScriptableObject
{
    [SerializeField] private EntityRace _race;
    [SerializeField] private EntityStats _stats;


    public EntityRace Race => _race;
    public EntityStats Stat => _stats;
}
