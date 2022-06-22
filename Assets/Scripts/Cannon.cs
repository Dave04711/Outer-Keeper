using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] Transform barrel;
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform shootOriginPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] float fireRate;
    [SerializeField] LayerMask targetMask;
    public float range;
    [SerializeField] int damage;
    [SerializeField] Animator animator;

    float nextTickTime = 0;
    Transform target;
    bool targeted;

    [SerializeField] ShootType shootType; //TOFIX: Clean up
    public Placement placementType;

    [SerializeField] int maxAmmo = 10;
    [SerializeField] int currentAmmo = 10;

    [Header("SG")]
    [SerializeField] int pellet = 5;
    [SerializeField] float spray = 5;
    [SerializeField] Animator baseAnimator;

    [Header("Tornado")]
    [SerializeField] float XW = 5;
    [SerializeField] float XH = 1;
    [SerializeField] float YW = 1;
    [SerializeField] float YH = 5;

    private void Start()
    {
        UIContainer.instance.ammoBar.gameObject.SetActive(true);
        SetBar();
    }

    private void Update()
    {
        if (!targeted)
        {
            Collider2D[] targets = null;
            if(shootType == ShootType.longRange)
            {
                List<Collider2D> ds = new List<Collider2D>();
                targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(range, XH), 0, targetMask);
                for (int i = 0; i < targets.Length; i++)
                {
                    ds.Add(targets[i]);
                }
                targets = Physics2D.OverlapBoxAll(transform.position, new Vector2(YW, range), 0, targetMask);
                for (int i = 0; i < targets.Length; i++)
                {
                    ds.Add(targets[i]);
                }
                targets = ds.ToArray();
            }
            else
            {
                targets = Physics2D.OverlapCircleAll(transform.position, range, targetMask);
            }
            if (targets.Length > 0)
            {
                targeted = true;
                target = targets[0].transform;
            } 
        }
        else if (targeted)
        {
            //Debug.Log(target.name);
            Rotate2Point(target);
            if (Time.time >= nextTickTime)
            {
                if(animator != null) { animator.SetTrigger("shoot"); }
                else { Shoot(); }
                nextTickTime = Time.time + 1f / fireRate;
            }
            if (!CheckTarget(target))
            {
                targeted = false;
            }
        }
    }

    void Rotate2Point(Transform _target)
    {
        if (shootType != ShootType.arc)
        {
            if (shootOriginPoint != null) { barrel.right = _target.position - shootOriginPoint.position; }
            else { barrel.right = _target.position - transform.position; }
        }
    }

    bool CheckTarget(Transform _target)
    {
        Health health = _target.GetComponent<Health>();
        if(health == null || !health.IsAlive()) { return false; }
        if(Vector2.Distance(transform.position,_target.position) > range) { return false; }
        return true;
    }

    public void Shoot()
    {
        if (CannonShopManager.instance.currentCannon.ammo > 0)
        {
            if (shootType == ShootType.shotgun)
            {
                for (int i = -pellet; i < pellet; i++)
                {
                    var _bullet = Instantiate(bullet, shootPoint.position, Quaternion.Euler(shootPoint.eulerAngles + Vector3.forward * i * spray));
                    _bullet.GetComponent<Bullet>().damage = damage;
                    Destroy(_bullet, 5f);
                    CannonShopManager.instance.currentCannon.ammo--;
                }
            }
            else
            {
                var _bullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
                _bullet.GetComponent<Bullet>().damage = damage;
                if (shootType == ShootType.arc)
                {
                    _bullet.GetComponent<BulletArc>().target = target;
                }
                Destroy(_bullet, 5f);
                CannonShopManager.instance.currentCannon.ammo--;
            } 
        }
        SetBar();
    }

    public void StartBaseAnim()
    {
        baseAnimator.SetTrigger("reload");
    }

    public void SetStats(float _speed, float _range, int _damage, int _ammo)
    {
        fireRate = _speed;
        range = _range;
        damage = _damage;
        maxAmmo = _ammo;
        Restock();
    }

    public void Restock()
    {
        CannonShopManager.instance.currentCannon.ammo = maxAmmo;
        SetBar();
    }

    public void SetBar()
    {
        UIContainer.instance.ammoBar.SetFill((float)CannonShopManager.instance.currentCannon.ammo / (float)maxAmmo);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
        Gizmos.DrawSphere(shootPoint.position, .25f);
        if(shootType == ShootType.longRange)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, new Vector2(range, XH));
            Gizmos.DrawWireCube(transform.position, new Vector2(YW, range));
        }
    }
}

public enum ShootType { straight, arc, shotgun, longRange }
public enum Placement { ground, air }