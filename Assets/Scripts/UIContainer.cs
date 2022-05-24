using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIContainer : MonoBehaviour
{
    #region Singleton
    public static UIContainer instance;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        else { Destroy(gameObject); }
    }
    #endregion

    public Animator animA;
    public ActionButton buttonA;
    public ActionButton buttonB;
    public GadgetButton buttonD;
    public GameOverPanel gameOverPanel;
}