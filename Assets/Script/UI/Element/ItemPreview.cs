using UnityEngine;
using UnityEngine.UI;

public class ItemPreview : MonoBehaviour
{
    [SerializeField] private string _defoutText;
    [Header("Reference")]
    [SerializeField] private Image _icon;
    [SerializeField] private Image _preview;
    [SerializeField] private TextUI _name;

    public void Reload()
    {
        SetData(_defoutText, null);
    }

    public void SetData(string name, Sprite icon)
    {
        _name?.SetText(name);
        ShowIcon(icon);
    }

    public void BindEntity(Entity entity)
    {
        if (entity)
            SetData(entity.Data.Name, entity.Data.Icon);
        else
            Reload();
    }

    private void ShowIcon(Sprite icon)
    {
        _icon.sprite = icon;
        _icon.gameObject.SetActive(icon);
        _preview.gameObject.SetActive(!icon);
    }
}
