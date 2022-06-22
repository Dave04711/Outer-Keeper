using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] protected float speed = 1;
    public int damage = 5;
    [SerializeField] protected float radius = .5f;
    [SerializeField] protected LayerMask targetMask;
    protected Rigidbody2D rb;
    [SerializeField] bool piercing = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (rb != null)
        { rb.velocity = transform.right * speed; }
        Destroy(gameObject, 8f);
    }

    private void Update()
    {
        if (rb != null)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, targetMask);
            if(hits.Length > 0)
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Health health = hits[i].GetComponent<Health>();
                    if (health != null)
                    {
                        health.Damage(damage);
                    }
                }
                if (!piercing) { Destroy(gameObject); }
                
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}