using System.Collections;
using UnityEngine;

public class Enemy : Entity, IPvp
{
    [SerializeField] private int _id;
    [SerializeField] private float _attackDistance;
    [SerializeField] private float _delay;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private LootSet _loot;
    [SerializeField] private EnemyMovement _movement;

    private Coroutine _steap;

    public override event System.Action OnComplite;

    public int Id => _id;
    public LootSet Loot => _loot;
    public EnemyMovement Movement => _movement;

    private void OnEnable()
    {
        _movement.OnCompliteMove += Complite;
    }

    private void OnDisable()
    {
        _movement.OnCompliteMove -= Complite;
    }


    public override void Play()
    {
        base.Play();
        if (_steap == null)
        {
            enabled = true;
            _steap = StartCoroutine(MakeSteap());
        }
    }

    public override void Stop()
    {
        base.Stop();
        enabled = false;
        Complite();
    }

    public void SetTarget(Player target)
    {
        _player = target;
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
            Attack(_player);
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
    }
}
