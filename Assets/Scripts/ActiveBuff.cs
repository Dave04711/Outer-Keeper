using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveBuff : MonoBehaviour
{
    MobileMovement mMovement;
    [SerializeField] float bSpeed = 1.4f;
    [SerializeField] float bDamage = 2;
    [SerializeField] float duration = 120;
    [SerializeField] Image speedBar;
    [SerializeField] Image damageBar;
    WaitForSeconds mWaitForSeconds;
    PlayerHealth playerHealth;
    PlayerActions playerActions;
    bool isBuffed = false;

    float defS;
    bool isBuffingSpeed = false;
    float activeTimeSpeed = 0f;

    int baseDmg;
    bool isBuffingDamage = false;
    float activeTimeDamage = 0f;

    private void Awake()
    {
        mMovement = GetComponent<MobileMovement>();
        mWaitForSeconds = new WaitForSeconds(duration);

        playerHealth = GetComponent<PlayerHealth>();

        playerActions = GetComponent<PlayerActions>();
    }

    private void Update()
    {
        if (isBuffingSpeed) { SpeedWait(); }
        if (isBuffingDamage) { DamageWait(); }
    }

    void HealthBuff()
    {
        playerHealth.Heal();
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
                    defS = mMovement.defSpeed;
                    mMovement.defSpeed *= bSpeed;
                    mMovement.SetMovement();
                    activeTimeSpeed = 0;
                    isBuffingSpeed = true;
                    break;
                case BuffType.Damage:
                    baseDmg = playerActions.damage;
                    playerActions.damage = (int)(bDamage * playerActions.damage);
                    activeTimeDamage = 0;
                    isBuffingDamage = true;
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

    void SpeedWait()
    {
        speedBar.transform.parent.gameObject.SetActive(true);
        activeTimeSpeed += Time.deltaTime;
        float percent = activeTimeSpeed / duration;
        speedBar.fillAmount = Mathf.Lerp(0, 1, 1f - percent);
        if (percent >= 1)
        {
            speedBar.fillAmount = 1;
            speedBar.transform.parent.gameObject.SetActive(false);
            isBuffingSpeed = false;
            mMovement.defSpeed = defS;
            mMovement.SetMovement();
        }
    }

    void DamageWait()
    {
        damageBar.transform.parent.gameObject.SetActive(true);
        activeTimeDamage += Time.deltaTime;
        float percent = activeTimeDamage / duration;
        damageBar.fillAmount = Mathf.Lerp(0, 1, 1f - percent);
        if (percent >= 1)
        {
            damageBar.fillAmount = 1;
            damageBar.transform.parent.gameObject.SetActive(false);
            isBuffingDamage = false;
            playerActions.damage = baseDmg;
        }
    }
}

public enum BuffType { Health, Speed, Damage, Cannons, Random }