using System.Collections.Generic;
using UnityEngine;

public class BattelLoder : MonoBehaviour
{
    [SerializeField] private float _spawnDistance;
    [Header("Reference")]
    [SerializeField] private Player _player;
    [SerializeField] private Battel _battel;
    [SerializeField] private EnemyHub _enemyHub;
    [SerializeField] private MapHolder _map;
    [SerializeField] private GameSwitcher _game;
    [SerializeField] private PvpControlelr _pvp;

    private List<Enemy> _prefabs = new List<Enemy>();

    private void Awake()
    {
        Load(_battel);
    }

    public void Load(Battel battel)
    {
        var points = _map.GetSpawnPoint(_player.transform.position, _spawnDistance);
        var list = new List<Entity>();
        _player.Stop();
        list.Add(_player);
        foreach (var id in battel.Enems)
        {
            var enemy = Instantiate(GetEnemy(id).gameObject).GetComponent<Enemy>();
            var point = points[Random.Range(0, points.Count)];
            points.Remove(point);
            enemy.Stop();
            enemy.SetTarget(_player);
            enemy.Movement.SetMap(_map);
            enemy.Movement.SetPoint(point);
            list.Add(enemy);
        }
        _pvp.SetParts(list);
    }

    private Enemy GetEnemy(int id)
    {
        foreach (var enemy in _prefabs)
        {
            if (enemy.Id == id)
                return enemy;
        }
        var prefab = _enemyHub.GetEnemy(id);
        _prefabs.Add(prefab);
        return prefab;
    }
}
