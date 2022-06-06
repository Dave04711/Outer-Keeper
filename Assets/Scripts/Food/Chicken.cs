using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Chicken : MonoBehaviour
{
    [SerializeField] int price;
    [SerializeField] TMP_Text priceTxt;

    private void Start()
    {
        priceTxt.text = price.ToString();
    }

    public void Buy()
    {
        CannonShopManager shopManager = CannonShopManager.instance;
        if(shopManager.cash < price)
        {
            shopManager.Rejection();
            return;
        }
        shopManager.SpendCash(price);
        Player.instance.GetComponent<PlayerActions>().damage += 2;
    }
}