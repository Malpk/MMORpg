using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private string _name;
    [Min(1)]
    [SerializeField] protected float moveDistance;
    [Min(1)]
    [SerializeField] protected float attackDistance;

    [SerializeField] private int _level;
    [SerializeField] private EntityStats _stats;
    [SerializeField] private Sprite _icon;
    [Header("Reference")]
    [SerializeField] private EntityBody _body;

    public int Level => _level;
    public int Attack => _stats.Strenght;
    public string Name => _name;
    public Sprite Icon => _icon;
    public EntityBody Body => _body;


}
