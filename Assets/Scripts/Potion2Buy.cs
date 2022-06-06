using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Potion2Buy : Item2Buy
{
    [SerializeField] TMP_Text priceTxt;
    [SerializeField] int price;
    [SerializeField] BuffType type;

    private void Start()
    {
        priceTxt.text = price.ToString();
    }

    protected override void DoStuff()
    {
        if (price <= CannonShopManager.instance.cash)
        {
            CannonShopManager.instance.SpendCash(price);
            Player.instance.GetComponent<ActiveBuff>()?.SetBuff(type);
        }
        else
        {
            CannonShopManager.instance.Rejection();
        }
    }
}
