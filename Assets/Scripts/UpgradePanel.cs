using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradePanel : MonoBehaviour
{
    [SerializeField] TMP_Text cannonName;
    [SerializeField] RectTransform iconParent;
    [Space]
    [SerializeField] Image damageBar;
    [SerializeField] TMP_Text damageTxt;
    [SerializeField] TMP_Text damagePrice;
    [Space]
    [SerializeField] Image rangeBar;
    [SerializeField] TMP_Text rangeTxt;
    [SerializeField] TMP_Text rangePrice;
    [Space]
    [SerializeField] Image speedBar;
    [SerializeField] TMP_Text speedTxt;
    [SerializeField] TMP_Text speedPrice;
    [Space]
    [SerializeField] Image ammoBar;
    [SerializeField] TMP_Text ammoTxt;
    [SerializeField] TMP_Text ammoPrice;
    [Space]
    [Space]
    [SerializeField] Color green;
    [SerializeField] Color red;

    private void OnEnable()
    {
        Init();
    }
    void Init()
    {
        cannonName.text = CannonShopManager.instance.currentCannon.name.Replace("(Clone)","");
        foreach (Transform child in iconParent)
        {
            Destroy(child.gameObject);
        }
        Instantiate(CannonShopManager.instance.currentCannon.iconPrefab, iconParent);
        SetBars();
    }

    public void UpgradeDamage()
    {
        CannonSO cur = CannonShopManager.instance.currentCannon;
        if(cur.damageU.currentLvl < 3)
        {
            if(CannonShopManager.instance.cash >= cur.damageU.levels[cur.damageU.currentLvl].price)
            {
                cur.damage = (int)cur.damageU.levels[cur.damageU.currentLvl].newValue;
                CannonShopManager.instance.SpendCash(cur.damageU.levels[cur.damageU.currentLvl].price);
                cur.damageU.currentLvl++;
                SetBars();
            }
            else
            {
                //need money
            }
        }
        else
        {
            //max lvl
        }
    }

    public void UpgradeRange()
    {
        CannonSO cur = CannonShopManager.instance.currentCannon;
        if (cur.rangeU.currentLvl < 3)
        {
            if (CannonShopManager.instance.cash >= cur.rangeU.levels[cur.rangeU.currentLvl].price)
            {
                cur.range = cur.rangeU.levels[cur.rangeU.currentLvl].newValue;
                CannonShopManager.instance.SpendCash(cur.rangeU.levels[cur.rangeU.currentLvl].price);
                cur.rangeU.currentLvl++;
                SetBars();
            }
            else
            {
                //need money
            }
        }
        else
        {
            //max lvl
        }
    }

    public void UpgradeSpeed()
    {
        CannonSO cur = CannonShopManager.instance.currentCannon;
        if (cur.speedU.currentLvl < 3)
        {
            if (CannonShopManager.instance.cash >= cur.speedU.levels[cur.speedU.currentLvl].price)
            {
                cur.speed = cur.speedU.levels[cur.speedU.currentLvl].newValue;
                CannonShopManager.instance.SpendCash(cur.speedU.levels[cur.speedU.currentLvl].price);
                cur.speedU.currentLvl++;
                SetBars();
            }
            else
            {
                //need money
            }
        }
        else
        {
            //max lvl
        }
    }

    public void UpgradeAmmo()
    {
        CannonSO cur = CannonShopManager.instance.currentCannon;
        if (cur.ammoU.currentLvl < 3)
        {
            if (CannonShopManager.instance.cash >= cur.ammoU.levels[cur.ammoU.currentLvl].price)
            {
                cur.ammo = (int)cur.ammoU.levels[cur.ammoU.currentLvl].newValue;
                CannonShopManager.instance.SpendCash(cur.ammoU.levels[cur.ammoU.currentLvl].price);
                cur.ammoU.currentLvl++;
                SetBars();
            }
            else
            {
                //need money
            }
        }
        else
        {
            //max lvl
        }
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Debug.Log($"{CannonShopManager.instance.currentCannon.name}" +
    //            $"dmg: {CannonShopManager.instance.currentCannon.damageU.currentLvl}:3 -> {CannonShopManager.instance.currentCannon.damage}" +
    //            $"rng: {CannonShopManager.instance.currentCannon.rangeU.currentLvl}:3 -> {CannonShopManager.instance.currentCannon.range}" +
    //            $"spd: {CannonShopManager.instance.currentCannon.speedU.currentLvl}:3 -> {CannonShopManager.instance.currentCannon.speed}" +
    //            $"amm: {CannonShopManager.instance.currentCannon.ammoU.currentLvl}:3 -> {CannonShopManager.instance.currentCannon.ammo}");
    //    }
    //}

    void SetBars()
    {
        CannonSO cur = CannonShopManager.instance.currentCannon;
        damageBar.fillAmount = (float)cur.damageU.currentLvl / 3f;
        rangeBar.fillAmount = (float)cur.rangeU.currentLvl / 3f;
        speedBar.fillAmount = (float)cur.speedU.currentLvl / 3f;
        ammoBar.fillAmount = (float)cur.ammoU.currentLvl / 3f;

        damageTxt.text = cur.damage.ToString();
        rangeTxt.text = cur.range.ToString();
        speedTxt.text = cur.speed.ToString();
        ammoTxt.text = cur.ammo.ToString();

        int dmgPrice = cur.damageU.levels[cur.damageU.currentLvl].price;
        int rngPrice = cur.rangeU.levels[cur.rangeU.currentLvl].price;
        int spdPrice = cur.speedU.levels[cur.speedU.currentLvl].price;
        int mmoPrice = cur.ammoU.levels[cur.ammoU.currentLvl].price;

        damagePrice.text = dmgPrice.ToString();
        rangePrice.text = rngPrice.ToString();
        speedPrice.text = spdPrice.ToString();
        ammoPrice.text = mmoPrice.ToString();

        if (dmgPrice <= CannonShopManager.instance.cash)
        {
            damagePrice.color = green;
        }
        else { damagePrice.color = red; }
        if (rngPrice <= CannonShopManager.instance.cash)
        {
            rangePrice.color = green;
        }
        else { rangePrice.color = red; }
        if (spdPrice <= CannonShopManager.instance.cash)
        {
            speedPrice.color = green;
        }
        else { speedPrice.color = red; }
        if (mmoPrice <= CannonShopManager.instance.cash)
        {
            ammoPrice.color = green;
        }
        else { ammoPrice.color = red; }
    }
}