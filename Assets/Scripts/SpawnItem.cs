using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] GameObject bear;
    [SerializeField] float speed = 1;
    bool isClose = false;
    [SerializeField] float radius = 4;
    [SerializeField] LayerMask targetLayer;
    Health target = null;
    float nextTickTime = 0;
    [SerializeField] float attackRate = 1f;
    [SerializeField] int damage = 10;
    [SerializeField] float duration = 100f;
    private void Start()
    {
        bear.SetActive(true);
        Destroy(gameObject, duration);
        bear.transform.eulerAngles = Vector3.up * 180;
    }

    private void Update()
    {
        isClose = Vector2.Distance(bear.transform.position, transform.position) < 2f;
        if (!isClose)
        {
            bear.transform.position = Vector2.MoveTowards(bear.transform.position, transform.position, Time.deltaTime * speed);
            bear.GetComponent<Animator>()?.SetBool("isMoving", true);
        }
        else
        {
            if(target == null || !target.IsAlive() || Vector2.Distance(target.transform.position, transform.position) > 2f)
            {
                bear.GetComponent<Animator>()?.SetBool("isMoving", false);
                target = Search4Target();
            }
            if(target != null && target.transform.position.x < bear.transform.position.x) { bear.transform.eulerAngles = Vector3.up * 180; }
            else if(target != null && target.transform.position.x > bear.transform.position.x) { bear.transform.eulerAngles = Vector3.zero; }
            if(target != null && Vector2.Distance(target.transform.position, bear.transform.position) > 2f)
            {
                bear.transform.position = Vector2.MoveTowards(bear.transform.position, target.transform.position, Time.deltaTime * speed);
                bear.GetComponent<Animator>()?.SetBool("isMoving", true);
            }
            else if(target != null && Vector2.Distance(target.transform.position, bear.transform.position) <= 2f)
            {
                bear.GetComponent<Animator>()?.SetBool("isMoving", false);
                if (Time.time >= nextTickTime)
                {
                    int rand = Random.Range(0, 2);
                    switch (rand)
                    {
                        default: bear.GetComponent<Animator>()?.SetTrigger("attack");break;
                        case 1: bear.GetComponent<Animator>()?.SetTrigger("attack2");break;
                    }
                    nextTickTime = Time.time + 1f / attackRate;
                }
            }
        }
    }

    public void Attack()
    {
        if (target != null)
        {
            target.Damage(damage);
        }
    }

    Health Search4Target()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);
        if(colliders.Length > 0)
        {
            Health enemy = null;
            for (int i = 0; i < colliders.Length; i++)
            {
                if(colliders[i].GetComponent<Health>() != null)
                {
                    enemy = colliders[i].GetComponent<Health>();
                    return enemy;
                }
            }
            return null;
        }
        return null;
    }
}