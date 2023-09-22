using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private Entity _entity;

    private void Start()
    {
        _entity.LoadEntity(_entity.Data, _entity.Stats);
    }


}
