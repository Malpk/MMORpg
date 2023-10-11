using UnityEngine;
using System.Collections;

public class EntityMovement : MonoBehaviour
{
    [Min(1)]
    [SerializeField] private float _moveDistance;
    [SerializeField] private float _speedMovement;
    [Header("Reference")]
    [SerializeField] private MapPoint _point;
    [SerializeField] private MapHolder _map;

    private Coroutine _corotine;

    public System.Action OnCompliteMove;

    public bool IsMove => _corotine != null;

    private void Awake()
    {
        transform.position = _point.transform.position;
        _point.SetContent(gameObject);
    }

    private void OnEnable()
    {
        _map.SetMap(_point.transform.position, _moveDistance);
        _map.OnActive += EnterPoint;
    }

    private void OnDisable()
    {
        _map.Reload();
        _map.OnActive -= EnterPoint;
    }


    private IEnumerator Move(Vector2 target)
    {
        while ((Vector2)transform.position != target)
        {
            transform.position = Vector2.MoveTowards(transform.position, target,
                 _speedMovement * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }
        OnCompliteMove?.Invoke();
        _point.SetContent(gameObject);
        _corotine = null;
    }

    public void SetTarget(Vector2 target)
    {
        if (!IsMove)
        {
            _corotine = StartCoroutine(Move(target));
        }
    }

    private void EnterPoint(MapPoint point)
    {
        if (!point.Content)
        {
            if (!IsMove)
            {
                SetTarget(point.transform.position);
                _point.SetContent(null);
                _point = point;
            }
        }
    }
}
