using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] GameObject item;
    public void DropItem()
    {
        Instantiate(item, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
