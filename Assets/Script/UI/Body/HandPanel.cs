using UnityEngine;

public class HandPanel : MonoBehaviour
{
    [SerializeField] private HandHolder _hands;
    [SerializeField] private PreviewIcon _weapon;
    [SerializeField] private PreviewIcon _shield;

    private void Awake()
    {
        _weapon.SetItem(_hands.Weapon);
        _hands.OnUpdateWeapon += _weapon.SetItem;
        _hands.OnUpdateShield += _shield.SetItem;
    }

    private void OnDestroy()
    {
        _hands.OnUpdateWeapon -= _weapon.SetItem;
        _hands.OnUpdateShield -= _shield.SetItem;
    }

}
