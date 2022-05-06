using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableCannon : Interacting
{
    [SerializeField] GameObject CannonCratePrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float waitTime = .5f;

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
}