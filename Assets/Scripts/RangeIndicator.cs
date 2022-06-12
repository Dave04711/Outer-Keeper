using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    LineRenderer circle;
    [HideInInspector] public float CircleRadius = 1;
    [SerializeField] protected int CircleSteps = 64;

    private void Awake()
    {
        circle = GetComponent<LineRenderer>();
    }

    private void OnEnable()
    {
        DrawCircle(CircleSteps, CircleRadius / 5);//or 10
    }

    protected void DrawCircle(int steps, float radius)
    {
        circle.positionCount = steps;

        for (int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumrefenceProgress = (float)currentStep / steps;
            float currentRadian = circumrefenceProgress * 2 * Mathf.PI;

            float xRange = Mathf.Cos(currentRadian);
            float yRange = Mathf.Sin(currentRadian);

            float X = xRange * radius;
            float Y = yRange * radius;

            Vector3 currentPosition = new Vector3(X, Y, 0);

            circle.SetPosition(currentStep, currentPosition);
        }
    }
}