using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAmmo : Interacting
{
    ActionButton liftBtn;
    [SerializeField] GameObject particles;

    private void Start()
    {
        GetComponent<Animator>().SetBool("hasAmmo", true);
        liftBtn = UIContainer.instance.buttonB;
    }

    public override void Interact()
    {
        Player.instance.GetComponent<PlayerActions>().enabled = false;
        Player.instance.GetComponent<Animator>().SetBool("ammo", true);
        UIContainer.instance.buttonB.SetIcon(1, 2f, 0f);
        UIContainer.instance.animA.SetBool("show", true);
        GetComponent<Animator>().SetBool("hasAmmo", false);
        liftBtn.onClickCallback = RefillAmmo;
    }

    void RefillAmmo()
    {
        float distance = Vector2.Distance(CannonShopManager.instance.currentCannonGameObject.transform.position, Player.instance.transform.position);
        if (distance <= 3f)
        {
            CannonShopManager.instance.currentCannonGameObject.GetComponent<Cannon>()?.Restock();
        }
        Destroy(Instantiate(particles, Player.instance.transform.position, Quaternion.identity), 3f);
        Player.instance.GetComponent<PlayerActions>().enabled = true;
        Player.instance.GetComponent<Animator>().SetBool("ammo", false);
        GetComponent<Animator>().SetBool("hasAmmo", true);
    }
}
