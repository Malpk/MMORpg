using UnityEngine;

public class Player : Entity, IPvp
{
    [SerializeField] private PvpSkills _skills;
    [SerializeField] private EntityMovement _movement;

    public event System.Action OnComplite;
    public event System.Action<AttackType> OnDamage;

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
        foreach (var part in body.Parts)
        {
            part.SetProtect(false);
        }
        _movement.enabled = true;
    }

    public void Skip()
    {
        _movement.enabled = false;
        Complite();
    }

    public void TakeDamage(int damage)
    {
        var attack = body.TakeDamage(damage);
        switch (attack)
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
    }

}
