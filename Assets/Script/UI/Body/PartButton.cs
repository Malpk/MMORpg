using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PartButton : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _selectButton;
    [SerializeField] private BodyPartPanel _panel;

    private bool _select;

    public event System.Action<PartButton> OnSelect;

    public PartType Part => _panel.Bind ? _panel.Bind.Part : PartType.None;

    private void Reset()
    {
        _selectButton = GetComponent<Image>();
    }

    public void Reload()
    {
        _select = false;
        _selectButton.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_select)
            _selectButton.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_select)
            _selectButton.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_select && _panel.Bind)
        {
            _select = true;
            OnSelect?.Invoke(this);
        }
    }
}
