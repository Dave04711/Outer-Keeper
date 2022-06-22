using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeRotate : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }
}