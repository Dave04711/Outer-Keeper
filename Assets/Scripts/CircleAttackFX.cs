using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttackFX : MonoBehaviour
{
    [SerializeField] float distance = 1.5f;
    private void Start()
    {
        var FX = GetComponent<ParticleSystem>();
        FX.startLifetime = distance / FX.startSpeed;
    }
}