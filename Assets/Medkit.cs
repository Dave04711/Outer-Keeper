using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    private void Start()
    {
        Player.instance.GetComponent<PlayerHealth>()?.Heal();
        Destroy(gameObject);
    }
}