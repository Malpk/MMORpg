using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkillRow : MonoBehaviour
{
    [SerializeField] private string _lableText;
    [SerializeField] private bool _hideAddButon = false;
    [Header("Reference")]
    [SerializeField] private Button _buttons;
    [SerializeField] private TextMeshProUGUI _lable;
    [SerializeField] private TextMeshProUGUI _value;

    private void Reset()
    {
        _lableText = "Название";
    }

    private void OnValidate()
    {
        _buttons.gameObject.SetActive(!_hideAddButon);
        _lable?.SetText(_lableText);
    }

    public void SetMode(bool mode)
    {
        _buttons.interactable = mode;
    }

    public void SetValue(int value)
    {
        _value.SetText(value.ToString());
    }

}
