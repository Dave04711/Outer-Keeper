using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathsManager : MonoBehaviour
{
    [SerializeField] List<Path> paths;
    [SerializeField] GameObject pathPrefab;
    public bool drawGiz = true;

    public static PathsManager instance;

    private void Awake()
    {
        if (instance != null) { Destroy(gameObject); }
        else { instance = this; }
    }
    public void CreatePath()
    {
        var path = Instantiate(pathPrefab, transform);
        path.GetComponent<Path>().Init();
        paths.Add(path.GetComponent<Path>());
    }

    public void TurnGizmos(bool _p)
    {
        foreach (var item in paths)
        {
            item.drawGizmos = _p;
        }
    }

    public Path GetRandomPath()
    {
        int rand = Random.Range(0, paths.Count);
        return paths[rand];
    }
}