using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour, IPvp
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _delay;
    [SerializeField] private float _radiusMove;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private MapPoint _point;
    [SerializeField] private MapHolder _map;
    [SerializeField] private EntityMovement _movement;

    private Coroutine _steap;

    public event System.Action OnComplite;

    private void Awake()
    {
        _point.SetContent(gameObject);
    }

    private void OnEnable()
    {
        _movement.OnCompliteMove += Complite;
    }

    private void OnDisable()
    {
        _movement.OnCompliteMove -= Complite;
    }

    public void Play()
    {
        if (_steap == null)
        {
            enabled = true;
            _steap = StartCoroutine(MakeSteap());
        }
    }

    public void Skip()
    {
        Complite();
    }

    private IEnumerator MakeSteap()
    {
        yield return new WaitForSeconds(_delay);
        var distance = Vector2.Distance(transform.position, _player.transform.position);
        if (distance > _attackDistance)
        {
            var target = _map.GetPoint(transform.position, _player.transform.position, _radiusMove);
            _movement.SetTarget(target.transform.position);
            _point.SetContent(null);
            _point = target;
        }
        else
        {
            _player.Body.TakeDamage(_damage);
            OnComplite?.Invoke();
            _steap = null;
        }
    }

    private void Complite()
    {
        OnComplite?.Invoke();
        if (_steap != null)
            StopCoroutine(_steap);
        _steap = null;
        _point.SetContent(gameObject);
        enabled = false;
    }
}
