using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShop : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject upgradePanel;
    [SerializeField] GameObject evolvePanel;
    [SerializeField] GameObject closeButton;

    private void OnEnable()
    {
        if(CannonShopManager.instance.currentCannon.upgrades.Count < 1) 
        {
            closeButton.SetActive(false);
            OpenUpgradePanel();
        }
    }

    public void OpenUpgradePanel()
    {
        upgradePanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void OpenEvolvePanel()
    {
        evolvePanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void OpenMainPanel()
    {
        upgradePanel.SetActive(false);
        evolvePanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    private void OnDisable()
    {
        OpenMainPanel();
    }
}