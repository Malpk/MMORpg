using UnityEngine;


public class BodyPartPanel : MonoBehaviour
{
    [SerializeField] private PartBody _bind;
    [Header("PrefabReference")]
    [SerializeField] private TextUI _health;
    [SerializeField] private ArmorPanel _armor;
    [SerializeField] private BodyPartPanelAnimator _animator;

    public PartBody Bind => _bind;

    private void OnEnable()
    {
        if (_bind)
        {
            SetHealth(_bind.Health, _bind.State);
            SetArmor(_bind.Armor);
            _bind.OnSetArmor += SetArmor;
            _bind.OnUpdateHealth += SetHealth;
            _bind.OnLoad += () => SetHealth(_bind.Health, _bind.State);
        }
        else
        {
            SetHealth(0, null);
            SetArmor(null);
        }
    }

    private void OnDisable()
    {
        if (_bind)
        {
            _bind.OnSetArmor -= SetArmor;
            _bind.OnUpdateHealth -= SetHealth;
            _bind.OnLoad -= () => SetHealth(_bind.Health, _bind.State);
        }
    }

    public void BindPanel(PartBody part)
    {
        SwitchPart(part);
        enabled = false;
        _bind = part;
        if (_bind)
        {
            SetArmor(_bind.Armor);
            SetHealth(_bind.Health, _bind.State);
        }
        else
        {
            SetArmor(null);
            SetHealth(0);
        }
        enabled = true;
    }

    public void Reload()
    {
        _animator.Reload();
    }

    public void SetArmor(Armor armor)
    {
        _armor.SetArmor(armor);
    }

    public void SetHealth(int health, BodyPartState state = null)
    {
        _health.SetText(health.ToString());
        _animator.SetState(state);
    }

    private void SwitchPart(PartBody part)
    {
        if (_bind)
        {
            _bind.OnSetArmor -= SetArmor;
            _bind.OnUpdateHealth -= SetHealth;
        }
        _bind = part;
        if (_bind)
        {
            _bind.OnSetArmor += SetArmor;
            _bind.OnUpdateHealth += SetHealth;
        }
    }
}
