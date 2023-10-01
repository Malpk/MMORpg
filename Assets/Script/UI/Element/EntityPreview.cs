using UnityEngine;
using UnityEngine.UI;

public class EntityPreview : MonoBehaviour
{
    [SerializeField] private string _defoutText;
    [Header("Reference")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _preview;
    [SerializeField] private TextUI _name;

    private Entity _bind;

    public void BindEntity(Entity entity)
    {
        _bind = entity;
        if (entity)
        {
            _name.SetText(entity.Name);
            ShowIcon(entity.Icon);
        }
        else
        {
            ShowIcon(null);
            _name.SetText(_defoutText);
        }
    }

    private void ShowIcon(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.gameObject.SetActive(icon);
        _preview.gameObject.SetActive(!icon);
    }
}
