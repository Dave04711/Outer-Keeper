using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveBuff : MonoBehaviour
{
    MobileMovement mMovement;
    [SerializeField] float bSpeed = 1.4f;
    [SerializeField] float bDamage = 2;
    [SerializeField] float duration = 120;
    WaitForSeconds mWaitForSeconds;
    PlayerHealth playerHealth;
    PlayerActions playerActions;
    bool isBuffed = false;

    private void Awake()
    {
        mMovement = GetComponent<MobileMovement>();
        mWaitForSeconds = new WaitForSeconds(duration);

        playerHealth = GetComponent<PlayerHealth>();

        playerActions = GetComponent<PlayerActions>();
    }

    IEnumerator SpeedBuff()
    {
        float defS = mMovement.defSpeed;
        mMovement.defSpeed *= bSpeed;
        yield return mWaitForSeconds;
        mMovement.defSpeed = defS;
    }

    void HealthBuff()
    {
        playerHealth.Heal();
    }

    IEnumerator AttackBuff()
    {
        int baseDmg = playerActions.damage;
        playerActions.damage = (int)(bDamage * playerActions.damage);
        yield return mWaitForSeconds;
        playerActions.damage = baseDmg;
    }

    public void SetBuff(BuffType buff)
    {
        if (!isBuffed)
        {
            switch (buff)
            {
                case BuffType.Health:
                    HealthBuff();
                    break;
                case BuffType.Speed:
                    StartCoroutine(SpeedBuff());
                    break;
                case BuffType.Damage:
                    StartCoroutine(AttackBuff());
                    break;
                case BuffType.Cannons:
                    break;
                case BuffType.Random:
                    break;
                default:
                    return;
            }
        }
        else
        {
            Debug.Log("don't overdose");
        }
    }
}

public enum BuffType { Health, Speed, Damage, Cannons, Random }