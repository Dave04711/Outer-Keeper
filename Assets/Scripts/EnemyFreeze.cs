using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFreeze : MonoBehaviour
{
    [SerializeField] Material freezeMat;
    [SerializeField] Material defMat;
    [SerializeField] SpriteRenderer sr;
    EnemyMove enemyMove;
    float speed;
    Health health;
    [SerializeField] Animator animator;

    private void Awake()
    {
        enemyMove = GetComponent<EnemyMove>();
        health = GetComponent<Health>();
        speed = enemyMove.speed;
    }

    public void Freeze()
    {
        StopAllCoroutines();
        StartCoroutine(FreezeCor());
    }

    IEnumerator FreezeCor()
    {
        health.Damage(1);
        sr.material = freezeMat;
        enemyMove.speed = 0;
        animator.speed = 0;
        yield return new WaitForSeconds(1.5f);
        sr.material = defMat;
        animator.speed = 1;
        enemyMove.speed = speed;
    }
}