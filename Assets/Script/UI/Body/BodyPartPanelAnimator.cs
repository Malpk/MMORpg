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

    public void SetState(BodyPartState part)
    {
        if (part != null)
        {
            _backGround.color = GetColor(part.State);
            _stateText?.SetText(part.StateName);
        }
        else
        {
            _backGround.color = GetColor(PartState.None);
            _stateText?.SetText(_noneText);
        }
    }

    private Color GetColor(PartState state)
    {
        switch (state)
        {
            case PartState.Idle:
                return _idle;
            case PartState.Wound:
                return _wound;
            case PartState.Break:
                return _break;
        }
        return _none;
    }
}
