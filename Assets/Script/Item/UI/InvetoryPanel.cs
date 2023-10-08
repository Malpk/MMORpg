using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public abstract class InvetoryPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _selectBackGround;
    [SerializeField] private ItemPreview _preview;

    private bool _isSelect;

    public event System.Action<InvetoryPanel> OnSelect;
    public event System.Action<InvetoryPanel> OnDeselect;


    public void Preview(string name, Sprite icon)
    {
        _preview.SetData(name, icon);
    }

    public void Deselect()
    {
        _isSelect = false;
        _selectBackGround.enabled = false;
    }

    #region Action

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelect = !_isSelect;
        if (_isSelect)
        {
            OnSelect?.Invoke(this);
            _selectBackGround.enabled = true;
        }
        else
        {
            OnDeselect?.Invoke(this);
            _selectBackGround.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!_isSelect)
            _selectBackGround.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!_isSelect)
            _selectBackGround.enabled = false;
    }
    #endregion
}
