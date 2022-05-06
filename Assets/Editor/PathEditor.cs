using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Path path = (Path)target;

        if (GUILayout.Button("Add point"))
        {
            path.AddPoint();
        }
    }
}