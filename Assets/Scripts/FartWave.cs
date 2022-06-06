using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartWave : Skill2Buy
{
    [Header("FartWave")]
    [SerializeField] float cooldown = 110;
    [SerializeField] GameObject fart;

    protected override void Skill()
    {
        SetCooldown(cooldown);
        fart.SetActive(true);
    }
}