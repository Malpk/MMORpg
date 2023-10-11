using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
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
        _point?.SetContent(gameObject);
    }

    public void SetMap(MapHolder map)
    {
        _map = map;
    }

    public void SetPoint(MapPoint point)
    {
        _point = point;
        _point.SetContent(gameObject);
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

    public void MoveToTarget(Transform target)
    {
        if (!IsMove)
        {
            _point.SetContent(null);
            _point = _map.GetPoint(transform.position, target.position, _moveDistance);
            _corotine = StartCoroutine(Move(_point.transform.position));
        }
    }
}
