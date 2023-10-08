using UnityEngine;

[CreateAssetMenu(menuName ="Entity/Race")]
public class RaceData : DescriptionData
{
    [SerializeField] private EntityRace _race;

    public EntityRace Race => _race;
}
