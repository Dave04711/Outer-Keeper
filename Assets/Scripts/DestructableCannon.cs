using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCannon : Interacting
{
    [SerializeField] GameObject CannonCratePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float waitTime = .5f;
    [SerializeField] GameObject particlesPrefab;

    private void Start()
    {
        CannonShopManager.instance.currentCannonGameObject = gameObject;
    }

    public override void Interact()
    {
        Player.instance.GetComponent<Animator>().SetTrigger("pack");
        StartCoroutine(SpawnCrate());
    }

    IEnumerator SpawnCrate()
    {
        yield return new WaitForSeconds(waitTime);
        var newCrate = Instantiate(CannonCratePrefab, spawnPoint.position, Quaternion.identity);
        newCrate.GetComponent<Animator>()?.SetTrigger("pack");
        Destroy(gameObject);
    }

    public void Vanish(Transform newPos)
    {
        Vector3 tarPos = newPos.position + newPos.right * 2f - Vector3.up * .2f;
        Destroy(Instantiate(particlesPrefab, transform.position, Quaternion.identity), 2f);
        Destroy(Instantiate(particlesPrefab, tarPos, Quaternion.identity), 2f);
        Instantiate(CannonCratePrefab, tarPos, Quaternion.identity);
        Destroy(gameObject);
    }
}