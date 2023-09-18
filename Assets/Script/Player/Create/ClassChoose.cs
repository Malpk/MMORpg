using UnityEngine;

public class ClassChoose : MonoBehaviour
{
    [SerializeField] private string _descriptionName;
    [Header("Reference")]
    [SerializeField] private ClassData[] _class;
    [SerializeField] private Description _description;

    public event System.Action<ClassData> OnChooseClass;

    #region Set
    public void SetWarrior()
    {
        SetClass(EntityClass.Warrior);
    }

    public void SetMagician()
    {
        SetClass(EntityClass.Magician);
    }

    public void SetRobber()
    {
        SetClass(EntityClass.Robber);
    }
    #endregion

    private void SetClass(EntityClass input)
    {
        foreach (var entityClass in _class)
        {
            if (entityClass.Class == input)
            {
                OnChooseClass?.Invoke(entityClass);
                _description.SetDescription(_descriptionName, entityClass.Desctiption);
                return;
            }
        }
    }
}
