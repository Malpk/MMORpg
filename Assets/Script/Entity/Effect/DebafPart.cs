using UnityEngine;
using System.Collections;

public abstract class DebafPart : MonoBehaviour
{
    [SerializeField] private bool _staticState;
    [SerializeField] private float _delay;
    [Header("State")]
    [SerializeField] private string _stateName;
    [SerializeField] private float _timeHeal;
    [SerializeField] private DamageType _damage;

    [SerializeField] protected float debaf;
    [SerializeField] protected DebafPartData debafActive;
    [Header("Reference")]
    [SerializeField] private DebafDataHolder _debafHolder;

    private float _progress;
    private Coroutine _heal;

    public event System.Action OnUpdateState;

    public abstract PartType Part { get; }

    public int Level => debafActive ? debafActive.Level : 0;
    public string StateName => _stateName;

    public abstract Stats AddDebaf(Stats stats, Stats baseStats);

    #region Save / Load
    public SavePartState Save()
    {
        var save = new SavePartState();
        if (debafActive)
        {
            save.Level = debafActive.Level;
            save.Debaf = debaf;
            save.Damage = _damage;
            save.Static = _staticState;
            save.HealProgress = _progress;
            save.HealTime = _timeHeal;
        }
        return save;
    }

    public void Load(SavePartState save)
    {
        if (save.Level > 0)
        {
            debafActive = _debafHolder.GetDebaf(save.Level);
            SetState(save.Damage);
            debaf = save.Debaf;
            _staticState = save.Static;
            _timeHeal = save.HealTime;
            _progress = save.HealProgress;
        }
    }
    #endregion

    public void TakeHeal(int level)
    {
        if (level >= Level)
        {
            debafActive = null;
            _stateName = "";
            _damage = DamageType.None;
            debaf = 0;
            if(_heal != null)
                StopCoroutine(_heal);
            OnUpdateState?.Invoke();
        }
    }

    public void TakeDamage(Attack attack)
    {
        var debaf = _debafHolder.GetDebaf();
        SetDebaf(debaf, attack);
        OnUpdateState?.Invoke();
    }

    private void SetDebaf(DebafPartData data, Attack attacl)
    {
        var debafLevel = debafActive ? debafActive.Level : 0;
        if (data.Level > debafLevel)
        {
            debafActive = data;
            debaf = debafActive.GetDebaf();
            SetState(attacl.DamageType);
            _staticState = false;
            _progress = 0f;
            _timeHeal = debafActive.GetTime();
        }
    }

    private void SetState(DamageType damage)
    {
        var state = _debafHolder.GetState(debafActive.Level, Part);
        _damage = damage;
        _stateName = state.GetName(damage);
    }

    public void StartHeal()
    {
        if (_heal != null)
            StopCoroutine(_heal);
        if (!_staticState)
        {
            if (debafActive)
            {
                _heal = StartCoroutine(Healing());
            }
        }
    }

    private IEnumerator Healing()
    {
        var timeHeal = _timeHeal * 3600f;
        var progress = 0f;
        while (_progress < 1f)
        {
            _progress += Time.deltaTime / timeHeal;
            progress += Time.deltaTime / _delay;
            if (progress >= 1)
                TryMakeStatic();
            yield return null;
        }
        _heal = null;
        TakeHeal(debafActive.Level);
    }

    private void TryMakeStatic()
    {
        var probility = Random.Range(0, 1f);
        if (probility > 0)
        {
            if (probility <= debafActive.ChanceMakeStatic)
            {
                _staticState = true;
                StopCoroutine(_heal);
                _heal = null;
            }
        }
    }
}
