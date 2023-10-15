using UnityEngine;

public class BaseDataSaver : MonoBehaviour
{
    [SerializeField] private bool _deleteMode;
    [SerializeField] private string _key = "key";
    [Header("Reference")]
    [SerializeField] private Entity _player;
    [SerializeField] private WalletSet _wallet;
    [SerializeField] private InvetorySet _invetory;

    private void Awake()
    {
        if(_deleteMode)
            PlayerPrefs.DeleteKey(_key);
    }

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
        var save = SaveData();
        PlayerPrefs.SetString(_key, JsonUtility.ToJson(save));
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            var save = JsonUtility.FromJson<SavePlayer>(
                PlayerPrefs.GetString(_key));
            LoadData(save);
        }
    }


    protected virtual SavePlayer SaveData()
    {
        var save = new SavePlayer();
        save.Money = _wallet.Money;
        save.Entity = _player.Save();
        save.Invetory = _invetory.Save();
        return save;
    }

    protected virtual void LoadData(SavePlayer save)
    {
        _player.Load(save.Entity);
        _wallet.SetMoney(save.Money);
        _invetory.Load(save.Invetory);
    }

}
