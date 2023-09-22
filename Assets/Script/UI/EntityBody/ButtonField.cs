using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonField : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private TMP_InputField _input;
    [Header("Event")]
    [SerializeField] private UnityEvent<int> _onClick;

    private int _value;

    private void OnEnable()
    {
        _input.onValueChanged.AddListener(SetValue);
    }

    private void OnDisable()
    {
        _input.onValueChanged.RemoveListener(SetValue);
    }

    private void Start()
    {
        _button.onClick.AddListener(()=> _onClick.Invoke(_value));
    }


    private void SetValue(string text)
    {
        _button.interactable = int.TryParse(text, out _value);
    }
}
