using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicatorMoving : RangeIndicator
{
    [SerializeField] float repeatTime = 1;
    [SerializeField] RangeIndicator rangeParent;
    private void OnEnable()
    {
        CircleRadius = rangeParent.CircleRadius;
        CancelInvoke();
        InvokeRepeating("Draw", 0, repeatTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
        StopAllCoroutines();
    }

    void Draw()
    {
        StopAllCoroutines();
        StartCoroutine(Drawing());
    }
    IEnumerator Drawing()
    {
        for (int i = 1; i <= 100; i++)
        {
            yield return null;
            DrawCircle(CircleSteps, CircleRadius * .002f * i);
        }
        DrawCircle(CircleSteps, 0);
    }
}