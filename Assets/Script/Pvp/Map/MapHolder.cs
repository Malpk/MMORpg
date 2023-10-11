using System.Collections.Generic;
using UnityEngine;

public class MapHolder : MonoBehaviour
{
    [SerializeField] private List<MapPoint> _points = new List<MapPoint>();

    public event System.Action<MapPoint> OnActive;
    public event System.Action<MapPoint> OnExit;

    private MapPoint Point { get;  set; }

    private void Reset()
    {
        _points.Clear();
        _points.AddRange(GetComponentsInChildren<MapPoint>());
    }

    private void OnEnable()
    {
        foreach (var point in _points)
        {
            point.OnActive += OnEnterPoint;
            point.OnExit += OnExitPoint;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _points)
        {
            point.OnActive -= OnEnterPoint;
            point.OnExit -= OnExitPoint;
        }
    }

    public void SetBlock(bool block)
    {
        foreach (var point in _points)
        {
            point.SetBlock(block);
        }
    }

    public void Reload()
    {
        Point?.Deactivate();
        Point = null;
    }

    public void SetMap(List<MapPoint> points)
    {
        _points = points;
    }

    #region Serach Way

    public void SetMap(Vector2 position,float moveRadius)
    {
        foreach (var point in _points)
        {
            if (!point.Content)
            {
                var distance = Vector2.Distance(point.transform.position, position);
                point.SetBlock(distance > moveRadius);
            }
            else
            {
                point.SetBlock(false);
            }
        }
    }

    public MapPoint GetPoint(Vector2 position, Vector2 target, float moveRadius)
    {
        var points = GetPoints(position, moveRadius);
        var closePoint = points[0];
        var closeDistance = Vector2.Distance(closePoint.transform.position, target);
        foreach (var point in points)
        {
            var distance = Vector2.Distance(point.transform.position, target);
            if (closeDistance > distance)
            {
                closeDistance = distance;
                closePoint = point;
            }
        }
        return closePoint;
    }

    public List<MapPoint> GetSpawnPoint(Vector2 postion, float radius)
    {
        var list = new List<MapPoint>();
        foreach (var point in _points)
        {
            if (!point.Content)
            {
                var distance = Vector2.Distance(point.transform.position, postion);
                if (distance > radius)
                {
                    list.Add(point);
                }
            }
        }
        return list;
    }

    private List<MapPoint> GetPoints(Vector2 position,float radius)
    {
        var list = new List<MapPoint>();
        foreach (var point in _points)
        {
            var distance = Vector2.Distance(point.transform.position, position);
            if (!point.Content)
            {
                if (radius >= distance)
                    list.Add(point);
            }
        }
        return list;
    }


    #endregion

    private void OnEnterPoint(MapPoint point)
    {
        if (Point != null)
            Point.Deactivate();
        Point = point;
        OnActive?.Invoke(Point);
    }

    private void OnExitPoint(MapPoint point)
    {
        Point = null;
        OnExit?.Invoke(null);
    }
}
