using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orb : Skill2Buy
{
    [Header("Orb")]
    [SerializeField] float cooldown = 10;
    [SerializeField] GameObject orb;

    protected override void Skill()
    {
        SetCooldown(cooldown);
        orb.SetActive(true);
    }
}