using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] Image fill;
    public void SetFill(float _percentage)
    {
        fill.fillAmount = _percentage;
    }
}