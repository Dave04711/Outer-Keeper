using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill2Buy : MonoBehaviour
{
    [SerializeField] protected int maxLvl = 3;
    [SerializeField] protected int currentLvl = 0;
    [Space]
    [SerializeField] protected Image barFill;
    [SerializeField] protected bool active = false;
    [SerializeField] protected Color maxedColor = new Color(255, 197, 0, 1);
    [SerializeField] protected GameObject priceObject;

    protected ActiveSkill skill;
    protected CannonShopManager shopManager;

    [SerializeField] protected Sprite skillIcon;

    private void Start()
    {
        skill = Player.instance.GetComponent<ActiveSkill>();
        shopManager = CannonShopManager.instance;
    }

    void SetBar()
    {
        barFill.fillAmount = (float)currentLvl / (float)maxLvl;
    }

    protected void ColorChange() 
    { 
        barFill.color = maxedColor;
        priceObject.SetActive(false);
    }

    protected void SetCooldown(float _param) { skill.cooldown = _param; }

    protected virtual void Skill() { Debug.Log("Used " + name); }
    protected virtual void PassiveUpgrade(int _lvl) 
    { 
        currentLvl++;
        SetBar();
    }

    public void Buy()
    {
        if(shopManager.metalShards <= 0)
        {
            shopManager.cashUI.Reject2();
            return;
        }
        if(currentLvl < maxLvl)
        {
            PassiveUpgrade(currentLvl);
            shopManager.SpendShards(1);
        }
        if (active && currentLvl == maxLvl)
        {
            skill.onSkillCallback = Skill;
            ColorChange();
            UIContainer.instance.skillIcon.sprite = skillIcon;
            return;
        }
    }
}