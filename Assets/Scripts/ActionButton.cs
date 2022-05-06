using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector] public bool buttonPressed;
    [HideInInspector] public float buttonHeld;
    [SerializeField] Sprite[] sprites;
    [SerializeField] Image icon;
    public Action onClickCallback;
    int index;

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        if(onClickCallback != null)
        {
            onClickCallback();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public void SetIcon(int _index, float scale = 2, float xOff = 0)
    {
        index = _index;
        icon.sprite = sprites[_index];
        icon.transform.localScale = Vector3.one * scale;
        icon.transform.position = transform.position + Vector3.right * xOff;
    }

    public int CheckIcon() { return index; }

    private void Update()
    {
        if (index != 0 && buttonPressed)
        {
            buttonHeld += Time.deltaTime;
        }
        else { buttonHeld = 0; }
    }
}