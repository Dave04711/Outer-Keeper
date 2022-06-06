using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherControl : Skill2Buy
{
    [Header("Weather Control")]
    [SerializeField] float cooldown = 10;
    [SerializeField] GameObject cloud;

    protected override void Skill()
    {
        SetCooldown(cooldown);
        Vector2 tarPos = Player.instance.transform.position + Player.instance.transform.right * 2 - Vector3.up;
        Instantiate(cloud, tarPos, Quaternion.identity);
    }
}
