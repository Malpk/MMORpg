using UnityEngine;
using UnityEngine.UI;

public class BodyPartPanelAnimator : MonoBehaviour
{
    [SerializeField] private string _noneText;
    [SerializeField] private Color _none;
    [SerializeField] private Color _idle;
    [SerializeField] private Color _break;
    [SerializeField] private Color _wound;
    [Header("Reference")]
    [SerializeField] private Image _backGround;
    [SerializeField] private TextUI _stateText;

    public void Reload()
    {
        SetState(null);
    }

    public void SetState(DebafPart part)
    {
        if (part != null)
        {
            _backGround.color = GetColor(part.Level);
            _stateText?.SetText(part.StateName);
        }
        else
        {
            _backGround.color = _none;
            _stateText?.SetText(_noneText);
        }
    }

    private Color GetColor(int level)
    {
        switch (level)
        {
            case 0:
                return _idle;
            case 1:
                return _wound;
            default:
                return _break;
        }
    }
}
