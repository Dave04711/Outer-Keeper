using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectThrow : MonoBehaviour
{
    BulletArc bulletArc;
    [SerializeField] Transform target;
    private void Awake()
    {
        bulletArc = GetComponent<BulletArc>();
        bulletArc.target = target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
        Gizmos.DrawWireSphere(target.position, .2f);
    }
}