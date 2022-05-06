using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFreezeRotation : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }
}