using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulation : Skill2Buy
{
    [Header("Time Manipulation")]
    [SerializeField] float cooldown = 60;
    [SerializeField] EnemySpawn enemySpawn;

    protected override void Skill()
    {
        SetCooldown(cooldown);
        enemySpawn.FreezeThem();
    }
}