using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image skillIcon;
}