using UnityEngine;
using UnityEngine.SceneManagement;

public class BattelMenu : MonoBehaviour
{
    [SerializeField] private int _pvpSceneId = 2;
    [Range(0,1f)]
    [SerializeField] private float _minPlayerHealth;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private MainDataSaver _saver;
    [SerializeField] private BattelPanel[] _panels;

    public event System.Action<Battel> OnStart;

    public void OnEnable()
    {
        SetMode(_player.HealthNormalize);
        _player.OnChangeHealth += SetMode;
        foreach (var panel in _panels)
        {
            panel.OnStart += StartBattel;
        }
    }

    private void OnDisable()
    {
        _player.OnChangeHealth -= SetMode;
        foreach (var panel in _panels)
        {
            panel.OnStart -= StartBattel;
        }
    }

    private void SetMode(float health)
    {
        foreach (var panel in _panels)
        {
            panel.SetMode(health > _minPlayerHealth);
        }
    }

    private void StartBattel(Battel battel)
    {
        _saver.SetBattel(battel);
        _saver.Save();
        SceneManager.LoadScene(_pvpSceneId);
    }
}
