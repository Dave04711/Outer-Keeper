using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantShop : Interacting
{
    [SerializeField] GameObject shopPanel;

    public override void Interact()
    {
        shopPanel.SetActive(true);
        PauseManager.instance.TimeSet(0);
    }
}