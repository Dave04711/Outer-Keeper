using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Path : MonoBehaviour
{
    public List<Transform> points;
    [SerializeField] GameObject pointPrefab;
    public bool drawGizmos;
    [SerializeField] Color gizmosColor;
    int counter = 0;

    //[ExecuteInEditMode]

    //private void Update()
    //{
    //    if (points.Contains(null)) { RemoveGhostPoints(); }
    //}
    public void Init()
    {
        gizmosColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
    }
    public void AddPoint()
    {
        Vector2 pos;
        if(points.Count > 0) { pos = points.Last().position; }
        else { pos = transform.position; }
        var newPoint = Instantiate(pointPrefab, pos, Quaternion.identity, transform);
        newPoint.name = $"Point {counter}";
        points.Add(newPoint.transform);
        counter++;
        newPoint.GetComponent<Point>()?.Init(this);
        #if UNITY_EDITOR
        Selection.objects = new GameObject[] { newPoint };
        #endif
    }

    //public void RemoveGhostPoints()
    //{
    //    points.RemoveAll(p => p == null);
    //}

    private void OnDrawGizmos()
    {
        points.RemoveAll(p => p == null);
        if (drawGizmos && points.Count > 1)
        {
            Gizmos.color = gizmosColor;
            for (int i = 0; i < points.Count - 1; i++)
            {
                Gizmos.DrawSphere(points[i].position, .05f);
                Gizmos.DrawLine(points[i].position, points[i + 1].position);
            }
            Gizmos.DrawCube(points[points.Count - 1].position, Vector2.one * .1f);
        }
    }
}