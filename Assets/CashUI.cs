using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CashUI : MonoBehaviour
{
    [SerializeField] TMP_Text cashTxt;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);//TODO: animator
    }
    public void ShowUI()
    {
        gameObject.SetActive(true);
    }

    public void ShowIncome(int _income)
    {
        StopAllCoroutines();
        StartCoroutine(ShowIncomeCor(_income));
    }

    public void ShowOutgo(int _outgo)
    {
        StopAllCoroutines();
        StartCoroutine(ShowOutgoCor(_outgo));
    }

    IEnumerator ShowIncomeCor(int _income)
    {
        int cash = int.Parse(cashTxt.text);
        int steps = CannonShopManager.instance.cash - cash;
        if(steps >= 100)
        {
            for (int i = 0; i <= steps; i++)
            {
                yield return null;
                cashTxt.text = (cash + i).ToString();
            }
        }
        else
        {
            for (int i = 0; i <= steps; i++)
            {
                yield return new WaitForSecondsRealtime(.04f);
                cashTxt.text = (cash + i).ToString();
            }
        }
    }

    IEnumerator ShowOutgoCor(int _outgo)
    {
        int cash = CannonShopManager.instance.cash;
        cashTxt.text = cash.ToString();
        for (int i = 0; i <= _outgo; i++)
        {
            yield return null;
            cashTxt.text = (cash - i).ToString();
        }
    }

    public void Reject()
    {
        animator.SetTrigger("reject");
    }
}