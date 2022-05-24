using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineEntry : Health
{
    [SerializeField] EnemySpawn enemySpawn;
    [SerializeField] GameObject particlesPrefab;
    [SerializeField] GameObject debris;
    [SerializeField] Collider2D entryCollider;
    [SerializeField] float wait4SpawnInSeconds;
    WaitForSeconds wait4Spawn;

    private void Awake()
    {
        wait4Spawn = new WaitForSeconds(wait4SpawnInSeconds);
    }

    private void Start()
    {
        ResetCave();
    }

    protected override void Die()
    {
        Destroy(Instantiate(particlesPrefab, transform.position + Vector3.up * .4f, Quaternion.identity), 3f);
        entryCollider.enabled = false;
        debris.SetActive(false);
        StartCoroutine(TurnOnSpawning());
    }

    public void ResetCave()
    {
        debris.SetActive(true);
        health = maxHP;
        entryCollider.enabled = true;
        enemySpawn.canSpawn = false;
    }

    IEnumerator TurnOnSpawning()
    {
        yield return wait4Spawn;
        enemySpawn.canSpawn = true;
    }
}