using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    public bool canSpawn = false;
    [SerializeField] int enemyCount = 50;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> spawnList = new List<GameObject>();
    [SerializeField] float spawnSpeed = .5f;//TODO: instead of spawnSpeed, should be separate class or something else what can set spawnSpeed individually to unit
    [SerializeField] List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] List<GameObject> unusedEnemies = new List<GameObject>();
    float nextSpawnTime = 0;
    float fraction;
    [SerializeField] TownHealth town;

    private void Awake()
    {
        fraction = 1f / spawnSpeed;
    }

    void Spawn()
    {
        var enemy = Instantiate(spawnList[Random.Range(0,spawnList.Count)], parent);
        spawnedEnemies.Add(enemy);
        enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
    }

    void Respawn(GameObject _enemy)
    {
        _enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
        TurnOn(_enemy);
    }

    public void TurnOff(GameObject _enemy)
    {
        town.DamageTown();
        _enemy.SetActive(false);
        unusedEnemies.Add(_enemy);
    }

    void TurnOn(GameObject _enemy)
    {
        if (unusedEnemies.Any())
        {
            unusedEnemies.Remove(_enemy);
        }
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime && canSpawn)
        {
            if (spawnedEnemies.Count < enemyCount)
            {
                Spawn();
            }
            else
            {
                //GameObject recycled = spawnedEnemies.Where(x => !x.activeInHierarchy).First();
                //Respawn(recycled);
                if(unusedEnemies.Any())
                {
                    Respawn(unusedEnemies.First());
                }
            }
            nextSpawnTime = Time.time + fraction;
        }
    }
}