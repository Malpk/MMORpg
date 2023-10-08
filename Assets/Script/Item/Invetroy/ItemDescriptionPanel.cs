using UnityEngine;

public class ItemDescriptionPanel : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private TextUI _delay;
    [SerializeField] private TextUI _probility;
    [SerializeField] private TextUI _description;
    [SerializeField] private ItemPreview _preview;

    public void SetMode(bool mode)
    {
        gameObject.SetActive(mode);
    }

    public void Reload()
    {
        _delay.SetText("Неизветсно");
        _probility.SetText("Неизвестно");
        _description.SetText("Неизвестно");
        _preview.Reload();
    }

    public void Preview(string name, Sprite icon)
    {
        _preview.SetData(name, icon);
    }

    public void SetDescription(string description)
    {
        _description.SetText(description);
    }

    public void SetUseData(int probility, string delay, int mana = 0)
    {
        _probility.SetText(probility.ToString());
        _delay.SetText(delay);
    }

}
