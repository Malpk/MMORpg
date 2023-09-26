using UnityEngine;

public class Entity : MonoBehaviour
{
    [Min(1)]
    [SerializeField] protected float moveDistance;
    [Min(1)]
    [SerializeField] protected float attackDistance;

    [SerializeField] private int _level;
    [SerializeField] private EntityStats _stats;
    [Header("Reference")]
    [SerializeField] private EntityBody _body;

    public int Level => _level;
    public int Health => _body.Health;
    public int Attack => _stats.Strenght;
    public EntityBody Body => _body;
}
