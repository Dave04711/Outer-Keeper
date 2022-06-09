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

    public void Heal()
    {
        health = maxHealth;
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