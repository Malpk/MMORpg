using UnityEngine;

[System.Serializable]
public class BodyPartStateData
{
    [Min(0)]
    [SerializeField] private int _seriousness;
    [SerializeField] private string _name;
    [SerializeField] private BodyPartState _state;

    public int Seriousness => _seriousness;
    public string StateName => _name;
    public BodyPartState State => _state;

}
