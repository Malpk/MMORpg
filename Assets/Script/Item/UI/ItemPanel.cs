using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemPanel : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Item _content;
    [Header("UI Reference")]
    [SerializeField] private Image _selectBackGround;
    [SerializeField] private TextUI _mana;
    [SerializeField] private TextUI _description;
    [SerializeField] private TextUI _setSkillScore;
    [SerializeField] private ItemPreview _preview;

    private bool _isSelect;

    public event System.Action<ItemPanel> OnSelect;
    public event System.Action<ItemPanel> OnDeselect;

    public Item Content => _content;

    public void Deselect()
    {
        _isSelect = false;
        _selectBackGround.enabled = false;
    }

    public void SetSkillSkore(int score)
    {
        _setSkillScore.SetText($"{score}/{_content.SkillScore}");
    }

    public void SetContent(Item item)
    {
        _content = item;
    }

    public void Preview(string name, Sprite icon)
    {
        _preview.SetData(name, icon);
    }

    public void SetDescription(string description)
    {
        _description.SetText(description);
    }

    public void SetMana(int mana)
    {
        _mana.gameObject.SetActive(mana > 0);
        _mana.SetText(mana.ToString());
    }

    #region Action

    public void OnPointerClick(PointerEventData eventData)
    {
        _isSelect = !_isSelect;
        if (_isSelect)
        {
            OnSelect?.Invoke(this);
        }
        else
        {
            OnDeselect?.Invoke(this);
            _selectBackGround.enabled = false;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isSelect)
            _selectBackGround.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_isSelect)
            _selectBackGround.enabled = false;
    }
    #endregion
}
