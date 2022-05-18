using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 10;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage( int _damage )
    {
        health -= _damage;
        if( health <= 0) { Die(); }
    }

    public void Heal()
    {
        health = maxHealth;
    }

    void Die()
    {
        Debug.Log("DIED");
    }
}