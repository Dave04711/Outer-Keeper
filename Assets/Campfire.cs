using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    [SerializeField] float radius = 4;
    float nextTickTime = 0;
    [SerializeField] float duration = 50;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance <= radius)
        {
            if (Time.time >= nextTickTime)
            {
                Player.instance.GetComponent<PlayerHealth>()?.Reg(1);
                nextTickTime = Time.time + 4f;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}