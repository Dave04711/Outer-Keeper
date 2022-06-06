using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonCrate : Interacting
{
    bool carried = false;
    SpriteRenderer spriteRenderer;
    Collider2D col;
    PlayerActions playerActions;
    MobileMovement movement;
    float cannonRadius;
    [SerializeField] Vector2 offset;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
    }
    private void Start()
    {
        playerActions = Player.instance.GetComponent<PlayerActions>();
        movement = Player.instance.GetComponent<MobileMovement>();
        cannonRadius = CannonShopManager.instance.currentCannon.range;
        CannonShopManager.instance.currentCannonGameObject = gameObject;
    }

    private void Update()
    {
        cannonRadius = CannonShopManager.instance.currentCannon.range;//TOFIX: tmp loop
    }

    public override void Interact()
    {
        TakeItOrLeaveIt();
    }

    void TakeItOrLeaveIt()
    {
        carried = !carried;
        spriteRenderer.enabled = !carried;
        col.enabled = !carried;
        transform.position = playerActions.transform.position + new Vector3(offset.x * playerActions.transform.right.x, offset.y);
        playerActions.loaded = carried;
        if (carried) 
        { 
            movement.SetSpeed();
            playerActions.SetRangeIndicator(cannonRadius);
            playerActions.SetLiftButtonIcon(1);
            Player.instance.GetComponent<PlayerUI>().speechBubble2.SetActive(false);
        }
        else 
        { 
            movement.SetMovement();
        }
    }
}