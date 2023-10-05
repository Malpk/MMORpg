using UnityEngine;

public class PvpSkillHolder : MonoBehaviour
{
    [SerializeField] private PvpSkillButton[] _buttons;

    private PvpSkillButton _button;

    private void Awake()
    {
        _button = _buttons[0];
        _buttons[0].Select();
    }

    private void OnEnable()
    {
        foreach (var button in _buttons)
        {
            button.OnSelect += Select;
        }
    }

    private void OnDisable()
    {
        foreach (var button in _buttons)
        {
            button.OnSelect -= Select;
        }
    }

    public void Select(PvpSkillButton button)
    {
        if (_button)
            _button.Deselect();
        _button = button;
    }
}
