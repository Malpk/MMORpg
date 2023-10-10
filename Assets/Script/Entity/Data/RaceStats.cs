using UnityEngine;

[CreateAssetMenu(menuName ="Entity/RaceStas")]
public class RaceStats : ScriptableObject
{
    [SerializeField] private EntityRace _race;
    [SerializeField] private Stats _stats;


    public EntityRace Race => _race;
    public Stats Stats => _stats;
}
