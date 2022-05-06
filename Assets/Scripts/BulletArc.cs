using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletArc : Bullet
{
    public AnimationCurve curve;
    [HideInInspector] public Transform target;

    Vector3 start;
    Coroutine coroutine;

    [SerializeField] float arcHeight = 3;
    [SerializeField] float boomRadius = 3;

    [SerializeField] GameObject boomFX;

    private void Awake()
    {
        start = transform.position;
    }

    private void Start()
    {
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Curve());
        }
    }

    private void Update()
    {
        
    }

    IEnumerator Curve()
    {
        float duration = 0.40f;
        float time = 0f;

        Vector3 end = target.position - (target.forward * 0.55f);

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, arcHeight, heightT);

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Impact();

        coroutine = null;
    }

    void Impact()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, boomRadius, targetMask);
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                Health health = hits[i].GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(damage);
                }
            }
            ShowBoom();
            Destroy(gameObject);
        }
        else
        {
            ShowBoom();
            Destroy(gameObject);
        }
    }

    void ShowBoom()
    {
        if (boomFX != null) { Destroy(Instantiate(boomFX, transform.position, Quaternion.identity), 3f); }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, boomRadius);
    }
}