using UnityEngine;

public class EntityHolder : MonoBehaviour
{
    [SerializeField] private RaceStats[] _races;

    public Stats GetStats(EntityRace input)
    {
        foreach (var race in _races)
        {
            if (race.Race == input)
            {
                return race.Stats;
            }
        }
        return default;
    }
}
