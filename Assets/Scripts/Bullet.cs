using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1;
    public int damage = 5;
    [SerializeField] float radius = .5f;
    [SerializeField] protected LayerMask targetMask;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (rb != null)
        { rb.velocity = transform.right * speed; }
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
                Destroy(gameObject);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}