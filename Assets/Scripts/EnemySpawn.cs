using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] bool canSpawn = false;
    [SerializeField] int enemyCount = 50;
    [SerializeField] Transform parent;
    [SerializeField] List<GameObject> spawnList = new List<GameObject>();
    [SerializeField] GameObject boss;
    [SerializeField] float spawnSpeed = .5f;//TODO: instead of spawnSpeed, should be separate class or something else what can set spawnSpeed individually to unit
    [SerializeField] List<GameObject> spawnedEnemies = new List<GameObject>();
    [SerializeField] List<GameObject> unusedEnemies = new List<GameObject>();
    float nextSpawnTime = 0;
    float fraction;
    [SerializeField] TownHealth town;
    [Space]
    [SerializeField] MineEntry mineEntry;
    [SerializeField] int increase = 7;
    int counter = 0;
    public int waveIndex = 0;
    int enemyMaxAmount;
    [SerializeField] GameObject overlay;

    private void Awake()
    {
        fraction = 1f / spawnSpeed;
    }

    void Spawn()
    {
        var enemy = Instantiate(spawnList[Random.Range(0,spawnList.Count)], parent);
        spawnedEnemies.Add(enemy);
        enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
        counter++;
    }

    void NewFraction() { fraction = 1f / spawnSpeed; }

    void Respawn(GameObject _enemy)
    {
        _enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
        TurnOn(_enemy);
        counter++;
    }

    public void TurnOff(GameObject _enemy)
    {
        town.DamageTown();
        if (_enemy.GetComponent<EnemyMove>().isBoss)
        {
            for (int i = 0; i < 15; i++)
            {
                town.DamageTown();
            }
        }
        _enemy.SetActive(false);
        unusedEnemies.Add(_enemy);
    }

    public void TurnOffEnemy(GameObject _enemy)
    {
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

    public void StartSpawning()
    {
        CannonShopManager.instance.GetComponent<TownHealth>().score = waveIndex;
        waveIndex++;
        enemyMaxAmount = waveIndex * increase;
        canSpawn = true;
        //reb
            if(waveIndex < 5) 
            { 
                spawnSpeed += .1f;
                NewFraction();
            }
            if (waveIndex % 5 == 0) { enemyCount += 5; }
            if ( waveIndex % 1 == 0 && spawnSpeed < 2f) 
            { 
                spawnSpeed += .1f;
                NewFraction();
                SpawnBoss();
            }
    }

    void SpawnBoss()
    {
        StopSpawning();
        var enemy = Instantiate(boss, parent);
        enemy.GetComponent<EnemyMove>().Init(PathsManager.instance.GetRandomPath());
        mineEntry.ResetCave();
    }

    public void StopSpawning()
    {
        canSpawn = false;
        enemyMaxAmount = 0;
        counter = 0;
    }

    public void FreezeThem()
    {
        StartCoroutine(FreezeThemCor());
    }

    IEnumerator FreezeThemCor()
    {
        EnemyMove[] enemyMoves = parent.GetComponentsInChildren<EnemyMove>();
        foreach (EnemyMove enemyMove in enemyMoves) { enemyMove.speed = 0; }
        canSpawn = false;
        overlay.SetActive(true);
        yield return new WaitForSeconds(10);
        foreach (EnemyMove enemyMove in enemyMoves) { enemyMove.Restore(); }
        canSpawn = true;
        overlay.SetActive(false);
    }

    private void Update()
    {
        if (!canSpawn) { return; }
        if (counter >= enemyMaxAmount) { mineEntry.ResetCave(); }
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