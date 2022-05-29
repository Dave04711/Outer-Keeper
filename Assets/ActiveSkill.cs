using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveSkill : MonoBehaviour
{
    public delegate void OnSkill();
    public OnSkill onSkillCallback;

    public void UseSkill()
    {
        if(onSkillCallback != null) { onSkillCallback(); }
    }
}