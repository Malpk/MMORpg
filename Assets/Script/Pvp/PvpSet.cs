using UnityEngine;
using UnityEngine.Events;

public class PvpSet : MonoBehaviour
{
    [SerializeField] private UnityEvent _onPlay;
    [SerializeField] private UnityEvent _onStop;

    public event System.Action OnComplite;

    public void Play()
    {
        _onPlay?.Invoke();
    }

    public void Stop()
    {
        _onStop?.Invoke();
        OnComplite?.Invoke();
    }
}
