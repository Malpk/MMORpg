using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private int _protect;
    [SerializeField] private Sprite _icon;
    [SerializeField] private PartType _part;

    public int Protect => _protect;
    public Sprite Icon => _icon;
    public PartType Part => _part;
}
