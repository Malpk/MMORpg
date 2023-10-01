using UnityEngine;

public class Player : Entity, IPvp
{
    [Header("Reference")]
    [SerializeField] private MapPoint _point;
    [SerializeField] private MapHolder _map;
    [SerializeField] private EntityMovement _movement;

    public event System.Action OnComplite;

    private void Awake()
    {
        transform.position = _point.transform.position;
        _point.SetContent(gameObject);
    }

    private void OnEnable()
    {
        _movement.OnCompliteMove += Complite;
        _map.OnActive += EnterPoint;
    }

    private void OnDisable()
    {
        _movement.OnCompliteMove -= Complite;
        _map.OnActive -= EnterPoint;
    }

    public void Play()
    {
        enabled = true;
        _map.SetMap(transform.position, moveDistance);
    }

    public void Skip()
    {
        enabled = false;
        Complite();
    }

    private void Complite()
    {
        _map.Reload();
        OnComplite?.Invoke();
    }

    private void EnterPoint(MapPoint point)
    {
        if (!point.Content)
        {
            if (!_movement.IsMove)
            {
                _movement.SetTarget(point.transform.position);
                _point = point;
                _point.SetContent(null);
                _point.SetContent(gameObject);
            }
        }
    }

}
