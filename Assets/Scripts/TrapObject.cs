using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapObject : Health
{
    private void Start()
    {
        health = maxHP;
    }
    protected override void Die()
    {
        Destroy(gameObject);
    }
}