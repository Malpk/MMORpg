using UnityEngine;

public class Player : Entity
{
    [SerializeField] private WalletSet _playerWallet;
    [SerializeField] private PvpSkills _skills;
    [SerializeField] private EntityMovement _movement;

    public event System.Action<AttackType> OnDamage;
    public override event System.Action OnComplite;

    public WalletSet Wallet => _playerWallet;

    private void OnEnable()
    {
        if(_movement)
            _movement.OnCompliteMove += Complite;
    }

    private void OnDisable()
    {
        if(_movement)
            _movement.OnCompliteMove -= Complite;
    }
    public override void Play()
    {
        base.Play();
        foreach (var part in body.Parts)
        {
            part.SetProtect(false);
        }
        _movement.enabled = true;
    }

    public override void Stop()
    {
        base.Stop();
        _movement.enabled = false;
        Complite();
    }

    public void TakeDamage(Attack damage)
    {
        var attack = body.TakeDamage(damage);
        switch (attack.Result)
        {
            case AttackType.Evasul:
                _skills.AddScore(PvpScoreType.Evasion);
                break;
            case AttackType.Protect:
                _skills.AddScore(PvpScoreType.Protect);
                break;
        }
    }

    private void Complite()
    {
        OnComplite?.Invoke();
        _movement.enabled = false;
    }

}
