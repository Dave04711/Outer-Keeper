using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerActions : MonoBehaviour
{
    Animator animator;
    [Header("Interacting")]
    [SerializeField] [Range(0, 2f)] float interactRange;
    [SerializeField] Transform interactPoint;
    [SerializeField] LayerMask interactLayerMask;
    [SerializeField] Transform center;
    ActionButton actionButton;
    ActionButton liftButton;
    Animator topButtonsAnimator;
    [HideInInspector] public bool loaded;
    Interacting trg, trg2;
    [SerializeField] RangeIndicator rangeIndicator;
    [Header("Attack")]
    public int damage = 1;
    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 1;
    [SerializeField] LayerMask attackLayerMask;
    [Header("Consumable")]
    public ItemSO item;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        actionButton = UIContainer.instance.buttonA;
        liftButton = UIContainer.instance.buttonB;
        topButtonsAnimator = UIContainer.instance.animA;

        liftButton.onClickCallback += Lift;
    }

    public void SetAttackState(bool _p)
    {
        animator.SetBool("work", _p);
    }

    private void Update()
    {
        //List<Collider2D> ds = new List<Collider2D>();
        //for (int i = 0; i < interactPoints.Length; i++)
        //{
        //    ds.Concat(Physics2D.OverlapBoxAll(interactPoints[i].position, Vector2.one * interactRange, interactLayerMask));
        //}
        if (!loaded)
        {
            List<Collider2D> ds = Physics2D.OverlapBoxAll(interactPoint.position, Vector2.one * interactRange, 0, interactLayerMask).ToList();
            if (ds.Count > 0)
            {
                List<Collider2D> crates = ds.Where(crate => crate.GetComponent<Interacting>()?.type == InteractType.Crate || crate.GetComponent<Interacting>()?.type == InteractType.Cannon).ToList();
                foreach (var crate in crates)
                {
                    ds.Remove(crate);
                }
                if(ds.Count > 0)
                {
                    Collider2D target = ds.OrderBy(col => Vector2.Distance(center.position, col.transform.position)).First();
                    trg = target.GetComponent<Interacting>();
                    if (trg != null)
                    {
                        int targetType = trg.CheckType();
                        switch (targetType)
                        {
                            default:
                                Debug.LogWarning($"Wrong interact type! < {gameObject.name} >");
                                topButtonsAnimator.SetBool("show", false);
                                break;
                            case 0:
                                actionButton.SetIcon(0);
                                topButtonsAnimator.SetBool("show", false);
                                break;
                            case 1:
                                actionButton.SetIcon(1);
                                topButtonsAnimator.SetBool("show", false);//TOFIX: clean up
                                break;
                            case 2: break;
                            case 3: break;
                        }
                    }
                }
                if(crates.Count > 0)
                {
                    Collider2D target2 = crates.OrderBy(col => Vector2.Distance(center.position, col.transform.position)).First();
                    trg2 = target2.GetComponent<Interacting>();
                    if (trg2 != null)
                    {
                        int targetType = trg2.CheckType();
                        switch (targetType)
                        {
                            default:
                                Debug.LogWarning($"Wrong interact type! < {gameObject.name} >");
                                topButtonsAnimator.SetBool("show", false);
                                break;
                            case 0: break;
                            case 1: break;
                            case 2:
                                topButtonsAnimator.SetBool("show", true);
                                SetLiftButtonIcon(0);
                                break;
                            case 3:
                                topButtonsAnimator.SetBool("show", true);
                                SetLiftButtonIcon(2);
                                break;
                        }
                    }
                }
            }
            else
            {
                trg = null;
                trg2 = null;
                actionButton.SetIcon(0);
                //SetLiftButtonIcon(0);
                topButtonsAnimator.SetBool("show", false);
            }
            switch (actionButton.CheckIcon())
            {
                case 0:
                    SetAttackState(actionButton.buttonPressed);
                    actionButton.onClickCallback = null;
                    break;
                case 1:
                    actionButton.onClickCallback = trg.Interact;
                    break;
            }
        }
        animator.SetBool("carry", loaded);
        rangeIndicator.gameObject.SetActive(loaded);
    }

    public void SetRangeIndicator(float _range)
    {
        rangeIndicator.CircleRadius = _range;
    }

    void Lift()
    {
        if (trg2 != null)
        {
            trg2.Interact();
        }
    }


    public void Attack()
    {
        //Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, attackLayerMask);
        Collider2D[] hits = Physics2D.OverlapBoxAll(attackPoint.position + Vector3.right * attackRange / 2, new Vector2(attackRange, 1), 0, attackLayerMask);
        for (int i = 0; i < hits.Length; i++)
        {
            Destructable destructable = hits[i].GetComponent<Destructable>();
            if(destructable != null) { destructable.Hit(damage); }
            EnemyMove enemyMove = hits[i].GetComponent<EnemyMove>();
            if(enemyMove != null) { enemyMove.Slowness(.7f); }
            Health health = hits[i].GetComponent<Health>();
            if(health != null) { health.Damage(damage); }
        }
    }

    public void SetLiftButtonIcon(int _index)
    {
        if (_index == 2)
        {
            liftButton.SetIcon(_index, 1.35f, -11.3f);
        }
        else
        {
            liftButton.SetIcon(_index, 2f, 0f);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(interactPoint.position, Vector2.one * interactRange);
        Gizmos.DrawSphere(center.position, .125f);
        Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        Gizmos.DrawWireCube(attackPoint.position + Vector3.right * attackRange / 2, new Vector2(attackRange, 1));
    }
}