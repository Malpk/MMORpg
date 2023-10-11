using UnityEngine;

public class EnemyHub : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;

    public Enemy GetEnemy(int id)
    {
        foreach (var prefab in _enemyPrefabs)
        {
            if (prefab.Id == id)
                return prefab;
        }
        return null;
    }
}
