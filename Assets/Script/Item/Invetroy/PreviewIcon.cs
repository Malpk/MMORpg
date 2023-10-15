using UnityEngine;
using UnityEngine.UI;

public class PreviewIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _preview;

    public void SetItem(Item item)
    {
        if(item)
            _icon.sprite = item.Icon;
        _icon.gameObject.SetActive(item);
        _preview.gameObject.SetActive(!item);
    }
}
