using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CannonCard : MonoBehaviour
{
    [SerializeField] List<Sprite> cardBG;
    public CannonSO cannonSO;
    [SerializeField] TMP_Text title;
    [SerializeField] Image cardImage;
    //[SerializeField] TMP_Text lvl;

    private void Start()
    {
        Init();
    }

    void Init()
    {
        title.text = cannonSO.name;
        cardImage.sprite = cardBG[Random.Range(0, cardBG.Count)];
        Instantiate(cannonSO.iconPrefab, transform);
        //lvl.text = cannonSO.rangeLvl.ToString();
    }

    public void ChangeCannon()
    {
        int metalShardsAmount = CannonShopManager.instance.metalShards;
        if(metalShardsAmount > 0)
        {
            CannonShopManager.instance.SpendShards(1);
            CannonShopManager.instance.currentCannon = cannonSO;
            CannonShopManager.instance.MakeSOClone();
            CannonShopManager.instance.cannonShop.gameObject.SetActive(false);
            PauseManager.instance.TimeSet();
        }
        else
        {
            CannonShopManager.instance.Rejection2();
        }
    }
}