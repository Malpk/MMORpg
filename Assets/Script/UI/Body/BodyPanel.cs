using UnityEngine;

public class BodyPanel : MonoBehaviour
{
    [SerializeField] private TextUI _amountProtect;
    [SerializeField] private BodyPartPanel[] _panels;

    public void BindParts(PartBody[] parts)
    {
        var amountProtect = 0;
        if (parts != null)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                _panels[i].BindPanel(parts[i]);
                if (parts[i].Armor)
                    amountProtect += parts[i].Armor.Protect;
            }
        }
        else
        {
            Clear();
        }
        _amountProtect?.SetText(amountProtect.ToString());
    }

    private void Clear()
    {
        foreach (var panel in _panels)
        {
            panel.BindPanel(null);
        }
    }
}
