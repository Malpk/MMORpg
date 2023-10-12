using UnityEngine;

public class MainDataSaver : BaseDataSaver
{
    private Battel _battel;

    public void SetBattel(Battel battel)
    {
        _battel = battel;
    }

    protected override SavePlayer SaveData()
    {
        var save = base.SaveData();
        save.Battel = _battel;
        return save;
    }
}
