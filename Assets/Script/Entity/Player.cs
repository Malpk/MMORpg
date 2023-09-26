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
    }

    private void OnDisable()
    {
        _movement.OnCompliteMove -= Complite;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _map.Point)
        {
            if (!_movement.IsMove)
            {
                _movement.SetTarget(_map.Point.transform.position);
                _point.SetContent(null);
                _point = _map.Point;
                _point.SetContent(gameObject);
            }
        }
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

    public void Complite()
    {
        _map.Reload();
        OnComplite?.Invoke();
    }
}
