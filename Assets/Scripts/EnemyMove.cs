using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    List<Transform> points;
    bool moving;
    public float speed = 1;
    float defSpeed;
    [SerializeField] Animator animator;
    int index = 1;
    [SerializeField] GameObject VanishFX;
    [Header("Attack")]
    [SerializeField] Transform attackPoint;
    [SerializeField] float range = 1;
    [SerializeField] LayerMask targetMask;
    [SerializeField] bool isAttacking = false;
    [SerializeField] float waitTimeMin = 0;
    [SerializeField] float waitTimeMax = 3;
    [SerializeField] int damage = 3;

    private void Awake()
    {
        defSpeed = speed;
    }

    public void Init(Path _path)
    {
        GetComponent<Health>().Respawn();
        points = _path.points;
        transform.position = points[0].position;
        index = 1;
    }

    private void Update()
    {
        if(index == points.Count)
        { 
            PathsManager.instance.GetComponent<EnemySpawn>().TurnOff(gameObject);
            Destroy(Instantiate(VanishFX, transform.position, Quaternion.identity), 2f);
        }
        else
        {
            if (Vector2.Distance(transform.position, points[index].position) <= .02f) { index++; }
            if (index < points.Count)
            {
                animator.SetBool("move", true);
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, points[index].position, step);
                Rotate(points[index].position.x < transform.position.x);
            }
            else
            {
                animator.SetBool("move", false);
            } 
        }
        if(IsCloseEnough().Length > 0 && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(Random.Range(waitTimeMin, waitTimeMax));
        var targets = IsCloseEnough();
        if(targets.Length <= 0) 
        {
            isAttacking = false;
        }
        else
        {
            speed = 0;
            animator.SetTrigger("attack");
            yield return new WaitForSeconds (.3f);
            foreach (var item in targets)
            {
                item.GetComponent<PlayerHealth>()?.TakeDamage(damage);
            }
            yield return new WaitForSeconds(.2f);
            Restore();
            isAttacking = false;
        }
    }

    Collider2D[] IsCloseEnough()
    {
        return Physics2D.OverlapCircleAll(attackPoint.position, range, targetMask);
    }

    void Rotate(bool _p)
    {
        if (_p)
        {
            transform.eulerAngles = Vector2.up * 180;
        }
        else
        {
            transform.eulerAngles = Vector2.zero;
        }
    }

    public void Slowness(float _p)
    {
        if(speed == defSpeed) { StartCoroutine(Slow(_p)); }
    }

    public void Restore() { speed = defSpeed; }

    IEnumerator Slow(float _percentage, float _time = 3f)
    {
        speed = defSpeed * _percentage;
        yield return new WaitForSeconds(_time);
        speed = defSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, range);
    }
}