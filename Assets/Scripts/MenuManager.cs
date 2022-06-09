using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    Animator animator;
    [SerializeField] TMP_Text setBtnTxt;
    bool setBtn = false;
    [SerializeField] TMP_Text highscoreTxt;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        if(PlayerPrefs.GetInt("controls") == 1) 
        {
            setBtnTxt.text = "D-pad";
        }
        ShowHighscore();
        if (!PlayerPrefs.HasKey("tutorial"))
        {
            PlayerPrefs.SetInt("tutorial", 0);
        }
    }

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame() { Application.Quit(); }

    public void SetBool(bool _p)
    {
        animator.SetBool("swap", _p);
    }

    public void SwapControls()
    {
        setBtn = !setBtn;
        
        switch (setBtn)
        {
            case true:
                setBtnTxt.text = "D-pad";
                PlayerPrefs.SetInt("controls", 1);
                break;
            case false:
                setBtnTxt.text = "Stick";
                PlayerPrefs.SetInt("controls", 0);
                break;
        }
    }

    public void ClearPrefs() 
    { 
        PlayerPrefs.DeleteAll();
        ShowHighscore();
        setBtn = false;
        setBtnTxt.text = "Stick";
    }

    void ShowHighscore()
    {
        if (PlayerPrefs.HasKey("highscore"))
        {
            highscoreTxt.text = $"Best: {PlayerPrefs.GetInt("highscore").ToString()}";
        }
        else { highscoreTxt.text = "Fresh meat!"; }
    }
}