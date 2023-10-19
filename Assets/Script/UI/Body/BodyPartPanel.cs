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
            SetHealth(_bind.Health);
            SetArmor(_bind.Armor);
            _bind.OnSetArmor += SetArmor;
            _bind.OnUpdateHealth += SetHealth;
            _bind.State.OnUpdateState += () => _animator.SetState(_bind.State);
            _bind.OnLoad += () => SetHealth(_bind.Health);
        }
        else
        {
            SetHealth(0);
            SetArmor(null);
        }
    }

    private void OnDisable()
    {
        if (_bind)
        {
            _bind.OnSetArmor -= SetArmor;
            _bind.OnUpdateHealth -= SetHealth;
            _bind.State.OnUpdateState -= () => _animator.SetState(_bind.State);
            _bind.OnLoad -= () => SetHealth(_bind.Health);
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
            SetHealth(_bind.Health);
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

    public void SetHealth(int health)
    {
        _health.SetText(health.ToString());
        if(_bind)
            _animator.SetState(_bind.State);
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
