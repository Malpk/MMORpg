using UnityEngine;
using System.Collections;

public class HealSet : MonoBehaviour
{
    [SerializeField] private bool _ready;
    [SerializeField] private float _timeReload;
    [SerializeField] private EntityBody _body;

    private float _progress = 0f;
    private Coroutine _reloading;

    public event System.Action OnReady;

    public bool IsReady => _ready;

    private void OnValidate()
    {
        OnReady?.Invoke();
    }

    private void Awake()
    {
        _body.OnLoad += StartHeal;
    }

    private void OnEnable()
    {
        _body.OnLoad += StartHeal;
    }

    private void OnDisable()
    {
        _body.OnLoad += StartHeal;
    }

    public void StartHeal()
    {
        foreach (var part in _body.Parts)
        {
            part.StartHeal();
        }
    }

    public bool Heal()
    {
        if (_ready)
        {
            _ready = false;
            foreach (var part in _body.Parts)
            {
                if (part.State.Level > 0)
                {
                    part.Heal();
                    StartRelaod();
                    return true;
                }
            }
        }
        return false;
    }

    private void StartRelaod()
    {
        if (_reloading != null)
            StopCoroutine(_reloading);
        _reloading = StartCoroutine(Reloading());
    }

    private IEnumerator Reloading()
    {
        while (_progress < 1f)
        {
            _progress += Time.deltaTime / _timeReload;
            yield return null;
        }
        _reloading = null;
        _ready = true;
        OnReady.Invoke();
    }

}
