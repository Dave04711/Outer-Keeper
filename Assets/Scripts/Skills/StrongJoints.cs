using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongJoints : Skill2Buy
{
    [Header("StrongJoints")]
    [SerializeField] MobileMovement playerMovement;
    [SerializeField] float cooldown = 10;
    protected override void PassiveUpgrade(int _lvl)
    {
        base.PassiveUpgrade(_lvl);
        switch (_lvl)
        {
            default:
                break;
            case 0:
                playerMovement.slowness = .9f;
                break;
        }
    }

    protected override void Skill()
    {
        SetCooldown(cooldown);
        DestructableCannonCrate crate = CannonShopManager.instance.currentCannonGameObject.GetComponent<DestructableCannonCrate>();
        if (crate != null) { crate.Vanish(Player.instance.transform); }
        DestructableCannon cannon = CannonShopManager.instance.currentCannonGameObject.GetComponent<DestructableCannon>();
        if (cannon != null) { cannon.Vanish(Player.instance.transform); }
    }
}