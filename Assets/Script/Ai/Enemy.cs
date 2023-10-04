using System.Collections;
using UnityEngine;

public class Enemy : Entity, IPvp
{
    [SerializeField] private int _damage;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _delay;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private EnemyMovement _movement;

    private Coroutine _steap;

    public event System.Action OnComplite;

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
            _movement.MoveToTarget(_player.transform);
        }
        else
        {
            _player.TakeDamage(_damage);
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
        enabled = false;
    }
}
