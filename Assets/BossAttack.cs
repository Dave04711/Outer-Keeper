using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    
    public void RockAttack()
    {
        StartCoroutine(Attack());
    }

    IEnumerator Attack()
    {
        Vector3 pos = Player.instance.transform.position;
        yield return new WaitForSeconds(.5f);
        if(transform.position.x > pos.x)
        {
            Destroy(Instantiate(prefab, pos, Quaternion.Euler(Vector3.up * 180)), 4f);
        }
        else
        {
            Destroy(Instantiate(prefab, pos, Quaternion.identity), 4f);
        }
    }
}