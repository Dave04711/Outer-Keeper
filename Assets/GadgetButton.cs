using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GadgetButton : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] Sprite nullSprite;
    PlayerActions playerActions;

    [SerializeField] TMP_Text quantityTxt;
    [SerializeField] Image bar;
    bool canUse = true;


    private void Start()
    {
        playerActions = Player.instance.GetComponent<PlayerActions>();
    }
    private void Awake()
    {
        
    }

    public void SetIcon()
    {
        ItemSO item = playerActions.item;
        if(item != null)
        {
            icon.sprite = item.icon;
            icon.rectTransform.localScale = Vector3.one * item.iconSize;
            quantityTxt.text = item.quantity.ToString();
            quantityTxt.enabled = true;
        }
        else
        {
            icon.sprite = nullSprite;
            icon.rectTransform.localScale = Vector3.one;
            quantityTxt.enabled = false;
            canUse = true;
        }
    }

    public void UseItem()
    {
        if (!canUse || playerActions.item == null) { return; }
        var itemObject = Instantiate(playerActions.item.itemPrefab, playerActions.transform.position, playerActions.transform.rotation);
        playerActions.item.quantity--;
        if(playerActions.item.quantity <= 0)
        {
            //Destroy(playerActions.item);
            playerActions.item = null;
        }
        else { StartCoroutine(Cooldown()); }
        SetIcon();
    }

    IEnumerator Cooldown()
    {
        canUse = false;
        float duration = playerActions.item.cooldown;
        for (int i = 0; i < duration * 100; i++)
        {
            bar.fillAmount = (float)i / (float)(duration * 100);
            yield return new WaitForSeconds(duration / 100f);
        }
        bar.fillAmount = 0;
        canUse = true;
    }
}