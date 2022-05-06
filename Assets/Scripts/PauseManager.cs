using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    #region Singleton
    public static PauseManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of PauseManager found!");
            Destroy(gameObject);
        }
        else { instance = this; }
    } 
    #endregion

    bool inUse = false;

    public void TimeSet(float _scale = 1)
    {
        if (!inUse || _scale == 1)
        {
            Time.timeScale = _scale;
            if (_scale != 1) { inUse = true; }
            else { inUse = false; } 
        }
    }
}