using UnityEngine;
using TMPro;

public class ExpSet : MonoBehaviour
{
    [SerializeField] private int _exp;
    [SerializeField] private ExpType _type;
    [Header("Reference")]
    [SerializeField] private TextMeshProUGUI _text;

    public int Exp => _exp;
    public ExpType Type => _type;

    public void SetExp(int exp)
    {
        _exp = exp;
        _text.SetText(_exp.ToString());
    }

    public void Add(int exp)
    {
        SetExp(_exp + exp);
    }

    public void Delete(int exp)
    {
        SetExp(Mathf.Clamp(_exp - exp, 0, _exp));
    }
}
