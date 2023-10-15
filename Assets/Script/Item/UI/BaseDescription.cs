using UnityEngine;

public class BaseDescription : MonoBehaviour
{
    [SerializeField] private ItemPreview _preview;
    [SerializeField] private TextUI _price;

    public void SetData(string name, Sprite icon, int price)
    {
        _price.SetText(price.ToString());
        _preview.SetData(name, icon);
    }

}
