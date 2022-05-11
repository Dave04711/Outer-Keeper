using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item2Buy : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        DoStuff();
    }

    protected virtual void DoStuff()
    {
        Debug.Log($"Clicked: {name}");
    }
}