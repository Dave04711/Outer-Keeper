using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBullet : Bullet
{

    [SerializeField] float dmgRate = 2;
    float nextTickTime = 0;
    [SerializeField] float duration = 10;

    private void Start()
    {
        if (rb != null)
        { rb.velocity = transform.right * speed; }
        Destroy(gameObject, duration);
    }
    void Update()
    {
        if (rb != null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
            if (hits.Length > 0 && Time.time >= nextTickTime)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Health health = hits[i].GetComponent<Health>();
                    if (health != null)
                    {
                        health.Damage(damage);
                    }
                }
                nextTickTime = Time.time + 1f / dmgRate;
            }
        }
    }
}