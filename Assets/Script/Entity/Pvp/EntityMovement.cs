using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    [SerializeField] private float _speedMovement;

    private Vector2 _target;

    public System.Action OnCompliteMove;

    public bool IsMove => enabled;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target,
            _speedMovement * Time.fixedDeltaTime);
        if ((Vector2)transform.position == _target)
        {
            enabled = false;
            OnCompliteMove?.Invoke();
        }
    }

    public void SetTarget(Vector2 target)
    {
        enabled = true;
        _target = target;
    }
}
