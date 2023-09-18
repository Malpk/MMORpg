using UnityEngine;

[CreateAssetMenu(menuName = "Entity/Class")]
public class ClassData : DescriptionData
{
    [SerializeField] private EntityClass _class;

    public EntityClass Class => _class;
}
