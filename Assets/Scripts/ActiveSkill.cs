using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveSkill : MonoBehaviour
{
    public delegate void OnSkill();
    public OnSkill onSkillCallback;
    public float cooldown = 1;
    public bool canUse = true;
    [SerializeField] Image bar;
    float activeTime = 0f;

    public void UseSkill()
    {
        if(onSkillCallback != null && canUse) 
        { 
            onSkillCallback();
            Cooldown();
        }
    }

    void Cooldown()
    {
        if (canUse)
        {
            activeTime = 0;
            canUse = false; 
        }
    }

    private void Update()
    {
        if (!canUse)
        {
            activeTime += Time.deltaTime;
            float percent = activeTime / cooldown;
            bar.fillAmount = Mathf.Lerp(0, 1, percent);
            if(percent >= 1)
            {
                bar.fillAmount = 0;
                canUse = true;
            }
        }
    }
}