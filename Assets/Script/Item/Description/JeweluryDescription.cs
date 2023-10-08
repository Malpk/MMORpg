using UnityEngine;

public class JeweluryDescription : BaseDescription
{
    [SerializeField] private TextUI _level;

    public void SetLevel(int level)
    {
        _level.SetText(level.ToString());
    }
}
