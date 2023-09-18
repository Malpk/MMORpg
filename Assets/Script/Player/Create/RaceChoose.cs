using UnityEngine;

public class RaceChoose : MonoBehaviour
{
    [SerializeField] private string _desctriptionName;
    [Header("Reference")]
    [SerializeField] private RaceData[] _races;
    [SerializeField] private Description _descrption;

    public event System.Action<RaceData> OnChooseRace;

    #region Set
    public void SetMen()
    {
        SetRace(EntityRace.Man);
    }

    public void SetElf()
    {
        SetRace(EntityRace.Elf);
    }

    public void SetDwarf()
    {
        SetRace(EntityRace.Dwarf);
    }
    #endregion

    private void SetRace(EntityRace input)
    {
        foreach (var race in _races)
        {
            if (race.Race == input)
            {
                OnChooseRace?.Invoke(race);
                _descrption.SetDescription(_desctriptionName, race.Desctiption);
                return;
            }
        }
    }
}
