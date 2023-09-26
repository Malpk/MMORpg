using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private Vector2 _offsetUnit;
    [SerializeField] private Vector2Int _mapSize;
    [SerializeField] private MapPoint _prefab;
    [SerializeField] private MapHolder _mapHolder;

    private List<MapPoint> _points = new List<MapPoint>();

    public MapPoint Point { get; private set; }

    private void Awake()
    {
         CreateMap();
    }

    public void CreateMap()
    {
        var height = _mapSize.y / 2;
        var width = (_mapSize.x) / 2;
        for (int i = -height; i <= height; i++)
        {
            var offset = Vector2.right * (Mathf.Abs(i) * _offsetUnit.x / 2) +
                Vector2.up * i * _offsetUnit.y;
            for (int j = -width; j < width-Mathf.Abs(i); j++)
            {
                var point = GetPoint();
                point.transform.localPosition = offset + Vector2.right *
                    (j * _offsetUnit.x + _offsetUnit.x / 2);
                _points.Add(point);
            }
        }
        _mapHolder.SetMap(_points);
    }

    public Vector2 GetPosition(Vector2 position)
    {
        var closePoint = _points[0].transform.position;
        var distance = Vector2.Distance(_points[0].transform.position, position);
        foreach (var point in _points)
        {
            var newDistance = Vector2.Distance(_points[0].transform.position, position);
            if (distance > newDistance)
            {
                closePoint = _points[0].transform.position;
                distance = newDistance;
            }
        }
        return closePoint;
    }

    private MapPoint GetPoint()
    {
        var point = Instantiate(_prefab.gameObject, transform).GetComponent<MapPoint>();
        return point;
    }


}
