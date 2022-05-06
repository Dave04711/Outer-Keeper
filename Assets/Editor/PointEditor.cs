using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Point))]
public class PointEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Point point = (Point)target;

        if (GUILayout.Button("Create point"))
        {
            point.AddPoint();
        }
    }
}
