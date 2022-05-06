using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelComponent : MonoBehaviour
{
    [SerializeField] Cannon cannon;
    MobileMovement mMovement;
    SpriteRenderer mRenderer;
    [SerializeField] bool orderChng;

    private void Start()
    {
        mMovement = Player.instance.GetComponent<MobileMovement>();
        mRenderer = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (orderChng)
        {
            if (mMovement.transform.position.y - .35f > transform.parent.position.y)
            {
                mRenderer.sortingOrder = 6;
            }
            else { mRenderer.sortingOrder = 4; } 
        }
    }

    public void Shoot()
    {
        cannon.Shoot();
    }

    public void BaseAnim()
    {
        cannon.StartBaseAnim();
    }
}