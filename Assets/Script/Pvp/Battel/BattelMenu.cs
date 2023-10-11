using UnityEngine;
using UnityEngine.SceneManagement;

public class BattelMenu : MonoBehaviour
{
    [SerializeField] private int _pvpSceneId = 2;
    [Header("Reference")]
    [SerializeField] private BattelPanel[] _panels;

    public event System.Action<Battel> OnStart;

    public void OnEnable()
    {
        foreach (var panel in _panels)
        {
            panel.OnStart += StartBattel;
        }
    }

    private void OnDisable()
    {
        foreach (var panel in _panels)
        {
            panel.OnStart -= StartBattel;
        }
    }

    private void StartBattel(Battel battel)
    {
        OnStart?.Invoke(battel);
        SceneManager.LoadScene(_pvpSceneId);
    }
}
