using UnityEngine;
using UnityEngine.SceneManagement;

public class EntityCreater : MonoBehaviour
{
    [SerializeField] private EntityData _data;
    [SerializeField] private Entity _entity;
    [SerializeField] private RaceStats[] _stats;
    [Header("Reference")]
    [SerializeField] private DataSaver _save;
    [SerializeField] private RaceChoose _raceMenu;
    [SerializeField] private ClassChoose _classMenu;
    [SerializeField] private GenderChoose _genderMenu;


    private void OnEnable()
    {
        _raceMenu.OnChooseRace += SetRace;
        _classMenu.OnChooseClass += SetClass;
        _genderMenu.OnChooseGender += SetGender;
    }

    private void OnDisable()
    {
        _raceMenu.OnChooseRace -= SetRace;
        _classMenu.OnChooseClass -= SetClass;
        _genderMenu.OnChooseGender -= SetGender;
    }

    public void CreateButton()
    {
        _entity.SetData(_data);
        _save.Save();
        SceneManager.LoadScene(1);
    }

    public EntityStats GetStats(EntityRace race)
    {
        foreach (var item in _stats)
        {
            if (item.Race == race)
                return item.Stat;
        }
        return default;
    }

    #region Data

    public void SetName(string name)
    {
        _data.Name = name;
    }

    public void SetIcon(Sprite sprite)
    {
        _data.Icon = sprite;
    }

    private void SetRace(RaceData data)
    {
        _data.Race = data.Race;
    }

    private void SetGender(EntityGender gender)
    {
        _data.Gender = gender;
    }

    private void SetClass(ClassData data)
    {
        _data.Class = data.Class;
    }
    #endregion
}
