using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Health : MonoBehaviour
{
    [SerializeField] protected int maxHP = 10;
    protected int health;
    [SerializeField] Animator animator;
    [Header("UI")]
    [SerializeField] HPBar healthBar;
    [Header("LOOT")]
    [SerializeField] GameObject itemPrefab;
    [SerializeField] GameObject metalShardPrefab;

    public void Damage(int _damage)
    {
        health -= _damage;
        if(health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        animator?.SetTrigger("death");
        GetComponent<Collider2D>().enabled = false;
        GetComponent<EnemyMove>().enabled = false;
        StartCoroutine(Dis());
    }

    IEnumerator Dis()
    {
        yield return new WaitForSeconds(.5f);
        DropItem();
        yield return new WaitForSeconds(1.5f);
        PathsManager.instance.GetComponent<EnemySpawn>().TurnOffEnemy(gameObject);
    }

    public void Respawn()
    {
        gameObject.SetActive(true);
        health = maxHP;
        GetComponent<Collider2D>().enabled = true;
        GetComponent<EnemyMove>().enabled = true;
        animator?.Rebind();
        animator?.Update(0);
    }

    public bool IsAlive()
    {
        return health > 0;
    }

    private void Update()
    {
        if(healthBar != null) { healthBar.SetFill((float)health / (float)maxHP); }
    }
    public void DropItem()
    {
        if(itemPrefab != null)
        {
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
        }
        if(metalShardPrefab != null)
        {
            if(Random.value < .01f)
            {
                Instantiate(metalShardPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}