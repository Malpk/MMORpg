using UnityEngine;

public class PlayerCreater : MonoBehaviour
{
    [SerializeField] private EntityData _enityData;
    [Header("Reference")]
    [SerializeField] private RaceChoose _race;
    [SerializeField] private ClassChoose _class;
    [SerializeField] private GenderChoose _gender;
    [SerializeField] private Description _desription;

    private void OnEnable()
    {
        _race.OnChooseRace += SetRace;
        _class.OnChooseClass += SetClass;
        _gender.OnChooseGender += SetGender;
    }

    private void OnDisable()
    {
        _race.OnChooseRace -= SetRace;
        _class.OnChooseClass -= SetClass;
        _gender.OnChooseGender -= SetGender;
    }

    public EntityData Create()
    {
        _enityData = new EntityData();
        return _enityData;
    }

    #region Data

    public void SetName(string name)
    {
        _enityData.Name = name;
    }

    public void SetIcon(Sprite sprite)
    {
        _enityData.Icon = sprite;
    }

    private void SetRace(RaceData data)
    {
        _enityData.Race = data.Race;
    }

    private void SetGender(EntityGender gender)
    {
        _enityData.Gender = gender;
    }

    private void SetClass(ClassData data)
    {
        _enityData.Class = data.Class;
    }
    #endregion
}
