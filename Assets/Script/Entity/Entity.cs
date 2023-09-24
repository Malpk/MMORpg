using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private int _level;
    [SerializeField] private EntityStats _stats;
    [Header("Reference")]
    [SerializeField] private EntityBody _body;

    public int Level => _level;
    public int Health => _body.Health;
    public int Attack => _stats.Strenght;
}
