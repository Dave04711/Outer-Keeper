using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Consumable2Buy : Item2Buy
{
    [SerializeField] ItemSO item;
    [SerializeField] TMP_Text priceTxt;

    private void Start()
    {
        priceTxt.text = item.price.ToString();
    }

    protected override void DoStuff()
    {
        if(item.price <= CannonShopManager.instance.cash)
        {
            var newItem = Instantiate(item);
            if(newItem.name == Player.instance.GetComponent<PlayerActions>().item?.name)
            {
                newItem.quantity += Player.instance.GetComponent<PlayerActions>().item.quantity;
            }
            Player.instance.GetComponent<PlayerActions>().item = newItem;
            //Player.instance.GetComponent<PlayerActions>().item = Instantiate(item);
            CannonShopManager.instance.SpendCash(item.price);
            UIContainer.instance.buttonD.SetIcon();
        }
        else
        {
            CannonShopManager.instance.Rejection();
        }
    }
}