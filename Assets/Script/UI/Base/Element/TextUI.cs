using UnityEngine;
using TMPro;

public class TextUI : MonoBehaviour
{
    [SerializeField] private string _content;
    [SerializeField] private string _prefix;
    [SerializeField] private string _postfix;
    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI _textMesh;

    public string Content => _content;

    private void OnValidate()
    {
        _textMesh?.SetText(_prefix + _content + _postfix);
    }

    public void SetText(string content)
    {
        _content = content;
        _textMesh.SetText(_prefix + _content + _postfix);
    }
}
