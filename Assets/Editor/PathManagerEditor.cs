using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PathsManager))]
public class PathManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        PathsManager paths = (PathsManager)target;

        paths.TurnGizmos(paths.drawGiz);

        if (GUILayout.Button("Create path"))
        {
            paths.CreatePath();
        }
    }
}