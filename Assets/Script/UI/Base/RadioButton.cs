using UnityEngine;
using TMPro;

public class RadioButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private RadioPoint[] _radioButtons;

    private RadioPoint _select;

    private void Start()
    {
        _radioButtons[Random.Range(0, _radioButtons.Length)].Select();
    }

    private void OnEnable()
    {
        foreach (var button in _radioButtons)
        {
            button.OnSelect += Select;
        }
    }

    private void OnDisable()
    {
        foreach (var button in _radioButtons)
        {
            button.OnSelect -= Select;
        }
    }

    private void Select(RadioPoint button)
    {
        if (_select)
            _select.Reload();
        _select = button;
        _text.SetText(_select.Description);
    }
}
