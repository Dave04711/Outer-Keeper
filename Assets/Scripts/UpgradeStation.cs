using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : Interacting
{
    Animator animator;
    [SerializeField] GameObject shopPanel;
    bool canUse = false;
    [SerializeField] Vector2 size;
    [SerializeField] LayerMask crateMask;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            animator.SetBool("on", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            animator.SetBool("on", false);
        }
    }

    public override void Interact()
    {
        if (canUse)
        {
            shopPanel.SetActive(true);
            PauseManager.instance.TimeSet(0);
        }
        else
        {
            StartCoroutine(ShowNHide(Player.instance.GetComponent<PlayerUI>().speechBubble));
        }
    }

    IEnumerator ShowNHide(GameObject _obj, float _time = 2f)
    {
        _obj.SetActive(true);
        yield return new WaitForSeconds(_time);
        _obj.SetActive(false);
    }

    private void Update()
    {
        Collider2D[] crates = Physics2D.OverlapBoxAll(transform.position, size, 0, crateMask);
        canUse = crates.Length == 1;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, size);
    }
}