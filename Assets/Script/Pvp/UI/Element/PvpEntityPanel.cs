using UnityEngine;

public class PvpEntityPanel : MonoBehaviour
{
    [SerializeField] private Entity _bindTarget;
    [Header("UI Reference")]
    [SerializeField] private BodyPanel _bodyPanel;
    [SerializeField] private EntityDataPanel _entityPanel;

    private void OnEnable()
    {
        if (_bindTarget)
        {
            BindPanel(_bindTarget);
            _bindTarget.OnLoad += () => BindPanel(_bindTarget);
        }
    }

    private void OnDisable()
    {
        if (_bindTarget)
        {
            _bindTarget.OnLoad -= () => BindPanel(_bindTarget);
        }
    }

    public void BindPanel(Entity entity)
    {
        _bindTarget = entity;
        _entityPanel.BindPanel(entity);
        _bodyPanel?.BindParts(entity ? entity.Body.Parts : null);
    }
}
