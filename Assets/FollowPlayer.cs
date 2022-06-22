using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] float maxDistance = 3;
    [SerializeField] float speed = 4;
    private void Update()
    {
        float distance = Vector2.Distance(transform.position, Player.instance.transform.position);
        if (distance > maxDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.instance.transform.position, speed * Time.deltaTime);
        }
    }
}