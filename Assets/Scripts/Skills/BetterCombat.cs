using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetterCombat : Skill2Buy
{
    [Header("BetterCombat")]
    [SerializeField] PlayerActions playerActions;
    protected override void PassiveUpgrade(int _lvl)
    {
        base.PassiveUpgrade(_lvl);
        switch (_lvl)
        {
            default:
                break;
            case 0:
                playerActions.slownessPercent = .5f;
                playerActions.damage += 1;
                break;
            case 1:
                playerActions.SwapAttackAnim(true);
                playerActions.damage += 2;
                playerActions.attackRange = 3;
                break;
            case 2:
                playerActions.damage += 1;
                playerActions.attackType = AttackType.circle;
                priceObject.SetActive(false);
                break;
        }
    }
}
