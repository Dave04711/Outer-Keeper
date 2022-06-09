using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeAmmo : Interacting
{
    [SerializeField] ActionButton liftBtn;

    private void Start()
    {
        GetComponent<Animator>().SetBool("hasAmmo", true);
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
        //TODO:
        //check distance
        //  A: Refill ammo
        //  B: particles
        //Reset
    }
}
