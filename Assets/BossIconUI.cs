using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossIconUI : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        InvokeRepeating("SwapIcon", 3, 3);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    void SwapIcon()
    {
        image.sprite = sprites[Random.Range(0, sprites.Length)];
    }
}