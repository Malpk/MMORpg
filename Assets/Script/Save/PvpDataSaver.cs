using UnityEngine;

public class PvpDataSaver : BaseDataSaver
{
    [SerializeField] private BattelLoder _loder;

    protected override void LoadData(SavePlayer save)
    {
        base.LoadData(save);
        _loder.Load(save.Battel);
    }
}
