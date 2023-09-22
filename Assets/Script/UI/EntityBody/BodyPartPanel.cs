using UnityEngine;
using UnityEngine.UI;

public class BodyPartPanel : MonoBehaviour
{
    [SerializeField] private PartType _part;
    [Header("Reference")]
    [SerializeField] private Image _image;
    [SerializeField] private TextUI _armor;
    [SerializeField] private TextUI _health;
    [SerializeField] private TextUI _damage;
    [SerializeField] private Animator _animator;

    public PartType Part => _part;

    public void Reload()
    {
        _animator.SetTrigger(GetAnimation(BodyPartState.Idle));
        _damage.SetText("");
    }

    public void SetArmor(Armor armor)
    {
        if (armor)
        {
            _image.sprite = armor.Icon;
            _armor.SetText(armor.Protect.ToString());
        }
        else
        {
            _armor.SetText("0");
        }
    }

    public void SetHealth(int health)
    {
        _health.SetText(health.ToString());
    }

    public void SetState(BodyPartStateData data)
    {
        _animator.SetTrigger(GetAnimation(data.State));
        _damage.SetText(data.StateName);
    }

    private string GetAnimation(BodyPartState state)
    {
        switch (state)
        {
            case BodyPartState.Wound:
                return "waound";
            case BodyPartState.Break:
                return "break";
        }
        return "idle";
    }

}
