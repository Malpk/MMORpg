using UnityEngine;

public class DescriptionData : ScriptableObject
{
    [TextArea(3,6)]
    [SerializeField] private string _description;

    public string Desctiption => _description;

}
