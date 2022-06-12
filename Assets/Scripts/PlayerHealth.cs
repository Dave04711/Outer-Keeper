using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int health;
    [SerializeField] int maxHealth = 10;
    [SerializeField] Image barFill;
    [SerializeField] TownHealth townHealth;
    [SerializeField] GameObject indicator;
    [SerializeField] GameObject indicator2;

    private void Start()
    {
        health = maxHealth;
        SetBar();
    }

    public void TakeDamage( int _damage )
    {
        health -= _damage;
        SetBar();
        StopAllCoroutines();
        StartCoroutine(ShowDmg());
        if( health <= 0) { Die(); }
    }

    IEnumerator ShowDmg()
    {
        indicator.SetActive( true );
        yield return new WaitForSeconds(.1f);
        indicator.SetActive( false );
        //yield return new WaitForSeconds(.1f);
        //indicator.SetActive( true );
        //yield return new WaitForSeconds(.1f);
        //indicator.SetActive( false );
    }

    IEnumerator ShowHeal()
    {
        indicator2.SetActive(true);
        yield return new WaitForSeconds(.1f);
        indicator2.SetActive(false);
    }

    public void Heal()
    {
        health = maxHealth;
        SetBar();
    }

    public void Reg(int _p)
    {
        if (health < maxHealth)
        {
            health += _p;
            StopAllCoroutines();
            StartCoroutine(ShowHeal());
        }
        SetBar();
    }

    public void SetHealth(int _p)
    {
        maxHealth += _p;
        health = maxHealth;
        SetBar();
    }

    void SetBar()
    {
        barFill.fillAmount = (float)health / (float)maxHealth;
    }

    void Die()
    {
        GetComponent<Animator>().SetTrigger("death");
        StopAllCoroutines();
        indicator.SetActive(false);
        StartCoroutine(EndGame());
    }
    
    IEnumerator EndGame()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 15; i++)
        {
            townHealth.DamageTown();
        }
    }
}