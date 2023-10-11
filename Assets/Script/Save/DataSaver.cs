using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private string _key = "key";
    [Header("Reference")]
    [SerializeField] private Entity _player;
    [SerializeField] private WalletSet _wallet;
    [SerializeField] private InvetorySet _invetory;
    [SerializeField] private BattelLoder _loder;

    private void Start()
    {
        Load();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        var save = new SavePlayer();
        save.Money = _wallet.Money;
        save.Entity = _player.Save();
        save.Invetory = _invetory.Save();
        PlayerPrefs.SetString(_key, JsonUtility.ToJson(save));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            var save = JsonUtility.FromJson<SavePlayer>(
                PlayerPrefs.GetString(_key));
            _player.Load(save.Entity);
            _wallet.SetMoney(save.Money);
            if(save.Invetory != null)
                _invetory.Load(save.Invetory);
            _loder?.Load(save.Battel);
        }
    }
}
