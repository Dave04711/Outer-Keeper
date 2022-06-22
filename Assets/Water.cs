using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Bullet
{
    [SerializeField] Transform dmgPoint;

    private void Start()
    {
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(dmgPoint.position, radius, targetMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Health health = hits[i].GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(damage);
                    health.wet = true;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(dmgPoint.position, radius);
    }
}