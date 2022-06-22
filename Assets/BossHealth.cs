using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : Health
{
    private void Start()
    {
        healthBar = UIContainer.instance.bossHPBar;
        healthBar.gameObject.SetActive(true);
    }

    protected override void Die()
    {
        healthBar.gameObject.SetActive(false);
        animator?.SetTrigger("death");
        Destroy(gameObject, 5f);
    }
}