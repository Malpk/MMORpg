using UnityEngine;

public class DataSaver : MonoBehaviour
{
    [SerializeField] private string _key = "key";
    [Header("Reference")]
    [SerializeField] private Player _player;

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
        PlayerPrefs.SetString(_key, _player.Save());
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey(_key))
        {
            var json = PlayerPrefs.GetString(_key);
            _player.Load(json);
        }
    }
}
