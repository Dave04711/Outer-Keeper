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
        cash += _p;
        cashUI.ShowIncome(_p);
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
}