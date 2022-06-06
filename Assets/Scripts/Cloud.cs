using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    [SerializeField] float duration = 10f;
    [SerializeField] Animator[] animators;
    [SerializeField] int damage = 3;
    private void Start()
    {
        Destroy(gameObject, duration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            Health health = collision.GetComponent<Health>();
            if(health == null) { return; }
            foreach (var anim in animators)
            {
                anim.SetTrigger("attack");
            }
            health.Damage(damage);
        }
    }
}
