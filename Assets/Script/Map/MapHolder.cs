using System.Collections.Generic;
using UnityEngine;

public class MapHolder : MonoBehaviour
{
    [SerializeField] private List<MapPoint> _points = new List<MapPoint>();

    public MapPoint Point { get; private set; }

    private void Reset()
    {
        _points.Clear();
        _points.AddRange(GetComponentsInChildren<MapPoint>());
    }

    private void OnEnable()
    {
        foreach (var point in _points)
        {
            point.OnActive += SetActive;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _points)
        {
            point.OnActive -= SetActive;
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
                point.SetMode(distance > moveRadius);
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

    private void SetActive(MapPoint point)
    {
        if (Point != null)
        {
            Point.Deactivate();
        }
        Point = point;
    }
}
