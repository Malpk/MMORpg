using UnityEngine;

public class HealSet : MonoBehaviour
{
    [SerializeField] private PartBody[] _parts;

    public void Heal()
    {
        foreach (var part in _parts)
        {
            if (part.State.Level > 0)
            {
                part.Heal();
                return;
            }
        }
    }

}
