using UnityEngine;

[CreateAssetMenu(menuName ="Entity/BodyPartState")]
public class BodyPartState : ScriptableObject
{
    [SerializeField] private string _name;
    [Min(0)]
    [SerializeField] private int _seriousness;
    [SerializeField] private PartState _state;

    public int Seriousness => _seriousness;
    public string StateName => _name;
    public PartState State => _state;


}
