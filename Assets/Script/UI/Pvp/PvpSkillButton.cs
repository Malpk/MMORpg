using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PvpSkillButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Image _select;
    [SerializeField] private TextUI _score;
    [SerializeField] private PvpSkillScore _content;
    [Header("Events")]
    [SerializeField] private UnityEvent<PvpSkillScore> _onChoose;

    private bool _isSelect;

    public event System.Action<PvpSkillButton> OnSelect;

    private void OnEnable()
    {
        SetScore(_content.Score);
        _content.OnUpdate += SetScore;
    }

    private void OnDisable()
    {
        _content.OnUpdate -= SetScore;
    }

    public void Select()
    {
        if (!_isSelect)
        {
            _isSelect = true;
            _select.enabled = true;
            OnSelect?.Invoke(this);
            _onChoose.Invoke(_content);
        }
    }

    public void Deselect()
    {
        _isSelect = false;
        _select.enabled = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!_isSelect)
            _select.enabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(!_isSelect)
            _select.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Select();
    }

    private void SetScore(int score)
    {
        _score.SetText(score.ToString());
    }
}
