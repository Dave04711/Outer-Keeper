using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShopManager : MonoBehaviour
{
    #region Singleton
    public static CannonShopManager instance;

    void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        else { instance = this; }
    }
    #endregion

    public CannonSO currentCannon;
    [Space]
    [SerializeField] Transform evolveCardParent;
    [SerializeField] GameObject cardPrefab;
    [Space]
    public CannonShop cannonShop;
    [Space]
    public int cash;
    public CashUI cashUI;
    public GameObject currentCannonGameObject;
    public bool doubleCash = false;

    public int metalShards;

    private void Start()
    {
        MakeSOClone();
    }

    public void SpawnCards()
    {
        foreach (Transform child in evolveCardParent)
        {
            Destroy(child.gameObject);
        }
        foreach (CannonSO CSO in currentCannon.upgrades)
        {
            var newCard = Instantiate(cardPrefab, evolveCardParent).GetComponent<CannonCard>();
            newCard.cannonSO = CSO;
        }
    }

    public void MakeSOClone()
    {
        currentCannon = Instantiate(currentCannon);
    }

    public void AddCash(int _p)
    {
        if (doubleCash)
        {
            int newCash = _p * 2;
            cash += newCash;
            cashUI.ShowIncome(newCash);
            return;
        }
        cash += _p;
        cashUI.ShowIncome(_p);
    }

    public void AddShards(int _p)
    {
        metalShards += _p;
        cashUI.UpdateTxt2();
    }

    public void SpendShards(int _p)
    {
        metalShards -= _p;
        cashUI.UpdateTxt2();
    }

    public void SpendCash(int _p)
    {
        cashUI.ShowOutgo(_p);
        cash -= _p;
    }

    public void Rejection()
    {
        cashUI.Reject();
    }
    public void Rejection2()
    {
        cashUI.Reject2();
    }
}