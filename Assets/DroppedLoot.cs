using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedLoot : MonoBehaviour
{
    public AnimationCurve curve;
    [HideInInspector] public Transform target;

    Vector3 start;
    Coroutine coroutine;

    [SerializeField] float arcHeight = 3;
    [SerializeField] float radius = 3;
    [Space]
    [SerializeField] int value = 10;

    bool tmp = false;

    private void Awake()
    {
        start = transform.position;
    }

    private void Start()
    {
        target = Player.instance.transform;
    }

    private void Update()
    {
        if(Vector2.Distance(start, target.position) < radius)
        {
            tmp = true;
        }
        if (tmp)
        {
            if (coroutine == null)
            {
                coroutine = StartCoroutine(Curve());
            }
        }
    }

    IEnumerator Curve()
    {
        float duration = 0.40f;
        float time = 0f;

        Vector3 end = target.position;

        while (time < duration)
        {
            time += Time.deltaTime;

            float linearT = time / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, arcHeight, heightT);

            end = target.position;

            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Impact();

        coroutine = null;
    }

    void Impact()
    {
        CannonShopManager.instance.AddCash(value);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}