using UnityEngine;

public class PaperDescription : BaseDescription
{
    [SerializeField] private TextUI _mana;

    public void SetAddition(int mana)
    {
        _mana.SetText(mana.ToString());
    }
}
