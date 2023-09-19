using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private EntityData _entityData;
    [SerializeField] private EntityStats _entiryStats;
    [Header("Reference")]
    [SerializeField] private SkillPanel _skillPanel;
    [SerializeField] private PlayerPanel _panel;

    public EntityData Data => _entityData;
    public EntityStats Stats => _entiryStats;

    private void OnEnable()
    {
        _skillPanel.OnSkillUpdate += SetStats;
    }

    private void OnDisable()
    {
        _skillPanel.OnSkillUpdate -= SetStats;
    }

    public void LoadEntity(EntityData data, EntityStats stats)
    {
        _entityData = data;
        _entiryStats = stats;
        _panel.LoadPlayer(_entityData);
        _skillPanel.SetStats(stats);
    }

    private void SetStats(EntityStats stats)
    {
        _entiryStats = stats;
    }

}
