using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartWaveObject : MonoBehaviour
{
    LineRenderer circle;
    [SerializeField] int steps = 50;
    [SerializeField] float radius = 1;
    [SerializeField] float duration = 1;
    bool canUse = true;
    float activeTime = 0;
    CircleCollider2D circleCollider;
    [SerializeField] MobileMovement movement;

    private void Awake()
    {
        circle = GetComponent<LineRenderer>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void OnEnable()
    {
        activeTime = 0;
        canUse = false;
    }

    private void Update()
    {
        DrawLoop();
    }

    void DrawLoop()
    {
        if (!canUse)
        {
            movement.Halt(true);
            activeTime += Time.deltaTime;
            float percent = activeTime / duration;
            DrawCircle(steps, radius * Mathf.Lerp(0, .2f, percent));
            circleCollider.radius = radius * Mathf.Lerp(0, .2f, percent);
            if (percent >= 1)
            {
                canUse = true;
                movement.Halt(false);
                gameObject.SetActive(false);
            }
        }
    }

    void DrawCircle(int steps, float radius)
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Health health = collision.GetComponent<Health>();
        if(health == null) { return; }
        health.Damage(100);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}