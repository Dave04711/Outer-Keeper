using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMaskAnimation : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    SpriteMask spriteMask;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteMask = GetComponent<SpriteMask>();
    }

    private void LateUpdate()
    {
        if (spriteMask != null && spriteRenderer != null)
        {
            spriteMask.sprite = spriteRenderer.sprite;
        }
    }
}