using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] int enemyCount = 50;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> spawnList = new List<GameObject>();
    [SerializeField] float spawnSpeed = .5f;//TODO: instead of spawnSpeed, should be separate class or something else what can set spawnSpeed individually to unit
    [SerializeField] List<GameObject> spawnedEnemies = new List<GameObject>();
    float nextSpawnTime = 0;

    void Spawn()
    {
        var enemy = Instantiate(spawnList[Random.Range(0,spawnList.Count)], parent);
        spawnedEnemies.Add(enemy);
        enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
    }

    void Respawn(GameObject _enemy)
    {
        _enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            if (spawnedEnemies.Count < enemyCount)
            {
                Spawn();
            }
            else
            {
                GameObject recycled = spawnedEnemies.Where(x => !x.activeInHierarchy).First();
                Respawn(recycled);
            }
            nextSpawnTime = Time.time + 1f / spawnSpeed;
        }
    }
}