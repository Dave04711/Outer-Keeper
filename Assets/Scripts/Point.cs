using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    Path path;

    public void Init(Path _path)
    {
        path = _path;
    }

    public void AddPoint()
    {
        path.AddPoint();
    }
}