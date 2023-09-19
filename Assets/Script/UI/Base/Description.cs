using UnityEngine;
using TMPro;

public class Description : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _name;
    [SerializeField] private TextMeshProUGUI _description;

    public void SetDescription(string name, string description)
    {
        _name.SetText(name);
        _description.SetText(description);
    }
}
