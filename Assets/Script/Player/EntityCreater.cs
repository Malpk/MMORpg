using UnityEngine;
using UnityEngine.Events;

public class EntityCreater : MonoBehaviour
{
    [SerializeField] private EntityData _entity;
    [SerializeField] private Entity _player;
    [SerializeField] private RaceStats[] _stats;
    [Header("Reference")]
    [SerializeField] private RaceChoose _raceMenu;
    [SerializeField] private ClassChoose _classMenu;
    [SerializeField] private GenderChoose _genderMenu;
    [Header("Event")]
    [SerializeField] private UnityEvent<EntityData> _onCreate;

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
        Create(_player);
    }

    public Entity Create(Entity entity)
    {
        entity.LoadEntity(_entity, GetStats(_entity.Race));
        _onCreate.Invoke(_entity);
        return entity;
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
        _entity.Name = name;
    }

    public void SetIcon(Sprite sprite)
    {
        _entity.Icon = sprite;
    }

    private void SetRace(RaceData data)
    {
        _entity.Race = data.Race;
    }

    private void SetGender(EntityGender gender)
    {
        _entity.Gender = gender;
    }

    private void SetClass(ClassData data)
    {
        _entity.Class = data.Class;
    }
    #endregion
}
