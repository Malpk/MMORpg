using UnityEngine;

public class ContentPanel : InvetoryPanel
{
    [SerializeField] private Item _content;
    [SerializeField] private TextUI _cost;
    [SerializeField] private TextUI _edurance;

    public Item Content => _content;

    public void SetContent(Item content)
    {
        _content = content;
    }

    public void SetData(int cost, int edurance)
    {
        _cost.SetText(cost.ToString());
        _edurance.SetText(edurance.ToString());
    }
}
