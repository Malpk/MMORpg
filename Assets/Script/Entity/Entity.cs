using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private int _level;
    [SerializeField] private Sprite _icon;
    [SerializeField] private EntityStats _stats;
    [Header("Reference")]
    [SerializeField] protected EntityBody body;

    public int Level => _level;
    public int Attack => _stats.Strenght;
    public string Name => _name;
    public Sprite Icon => _icon;
    public EntityBody Body => body;


}
