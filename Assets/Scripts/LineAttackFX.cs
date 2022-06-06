using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttackFX : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    [SerializeField] float distance = 3;
    [SerializeField] float speed = 1;
    Vector3 targetPos;

    private void OnEnable()
    {
        transform.position = spawnPoint.position - Vector3.up * .25f;
        targetPos = transform.position + Vector3.right * distance;
    }

    private void Update()
    {
        var step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, step);
        if (Vector3.Distance(transform.position, targetPos) < 0.001f)
        {
            gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }
}