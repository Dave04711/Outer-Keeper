using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    Animator animator;
    [SerializeField] Vector2 _size = Vector2.one;
    [SerializeField] int _damage = 10;
    [SerializeField] LayerMask _targetLayers;
    [SerializeField] float _damageWaitTime = .5f;
    const string _animParamName = "show";
    bool _isReady = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InvokeRepeating("DealDamage", 0, _damageWaitTime);
    }

    private void Update()
    {
        Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, _size, 0, _targetLayers);
        animator.SetBool(_animParamName, targets.Length > 0);
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }

    void DealDamage()
    {
        if (_isReady)
        {
            Collider2D[] targets = Physics2D.OverlapBoxAll(transform.position, _size, 0, _targetLayers);
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].GetComponent<Health>()?.Damage(_damage);
            } 
        }
    }

    public void SetReadyTrue() { _isReady = true; }
    public void SetReadyfalse() { _isReady = false; }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _size);
    }
}