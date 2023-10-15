using UnityEngine;

public class DescriptionHolder : MonoBehaviour
{
    [SerializeField] private ArmorDescription _armorDescription;
    [SerializeField] private PaperDescription _papaerDescription;
    [SerializeField] private PotionDescription _potionDescription;

    private GameObject _description;

    public void Show(Item item)
    {
        if (item is Armor armor)
        {
            ShowDescription(_armorDescription.gameObject);
            armor.BindDescription(_armorDescription);
        }
        else if (item is Paper paper)
        {
            ShowDescription(_papaerDescription.gameObject);
            paper.BindDescription(_papaerDescription);
        }
        else if (item is Potion polution)
        {
            ShowDescription(_potionDescription.gameObject);
            polution.BindDescription(_potionDescription);
        }
    }

    public void Hide()
    {
        _description?.SetActive(false);
    }

    private void ShowDescription(GameObject description)
    {
        _description?.SetActive(false);
        _description = description;
        _description.SetActive(true);
    }
}
