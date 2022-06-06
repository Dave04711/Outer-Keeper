using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DestructableCannonCrate : Destructable
{
    bool canBuild = true;
    [SerializeField] float radius = .5f;
    [SerializeField] Transform centerPoint;
    [SerializeField] GameObject particlesPrefab;

    public override void Hit(int _damage)
    {
        if (canBuild)
        {
            base.Hit(_damage);
        }
        else
        {
            StartCoroutine(ShowIcon());
        }
    }
    protected override void Destruct()
    {
        base.Destruct();
        Invoke("SpawnCannon", .35f);
    }

    void SpawnCannon() 
    { 
        var cannon = Instantiate(CannonShopManager.instance.currentCannon.cannonPrefab, transform.position, Quaternion.identity);
        cannon.GetComponent<Cannon>()?.SetStats(CannonShopManager.instance.currentCannon.speed, CannonShopManager.instance.currentCannon.range, CannonShopManager.instance.currentCannon.damage);
    }

    IEnumerator ShowIcon()
    {
        GameObject sb = Player.instance.GetComponent<PlayerUI>().speechBubble2;
        sb.SetActive(true);
        yield return new WaitForSeconds(2);
        sb.SetActive(false);
    }

    public void Vanish(Transform newPos)
    {
        Vector3 tarPos = newPos.position + newPos.right * 2f - Vector3.up * .2f;
        Vector3 offset = new Vector3(.15f, -.5f, 0);
        Destroy(Instantiate(particlesPrefab, transform.position + offset, Quaternion.identity), 2f);
        Destroy(Instantiate(particlesPrefab, tarPos + offset, Quaternion.identity), 2f);
        transform.position = tarPos;
    }

    private void Update()
    {
        Collider2D[] ds = Physics2D.OverlapCircleAll(centerPoint.position, radius);
        List<Collider2D> list = ds.Where(col => col.gameObject.layer == 4 || col.gameObject.layer == 10).ToList();
        if(CannonShopManager.instance.currentCannon.cannonPrefab.GetComponent<Cannon>().placementType == Placement.ground) { canBuild = list.Count == 0; }
        else { canBuild = true; }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerPoint.position, radius);
    }
}