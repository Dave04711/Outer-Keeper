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
                playerActions.damage = 2;
                break;
            case 1:
                Debug.Log("poziom 2B");
                break;
            case 2:
                Debug.Log("poziom 3C");
                break;
        }
    }
}
