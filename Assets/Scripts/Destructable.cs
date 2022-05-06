using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    [SerializeField] int HP = 3;

    Animator animator;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void Hit(int _damage)
    {
        HP -= _damage;
        if(HP <= 0)
        {
            Destruct();
        }
        else
        {
            animator.SetTrigger("hurt");
        }
    }

    protected virtual void Destruct()
    {
        animator.SetBool("destroy", true);
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        WaitForSeconds tick = new WaitForSeconds(.1f);
        yield return new WaitForSeconds(3);
        for (int i = 0; i < 25; i++)
        {
            yield return tick;
            spriteRenderer.color = spriteRenderer.color * new Vector4(1, 1, 1, .75f);
        }
        Destroy(gameObject);
    }
}