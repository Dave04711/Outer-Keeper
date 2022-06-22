using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bear : MonoBehaviour
{
    [SerializeField] SpawnItem spawnItem;
    public void Attack()
    {
        spawnItem.Attack();
    }
}