using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownHealth : MonoBehaviour
{
    int townHealth = 15;
    [SerializeField] int townHealthMax = 15;
    [SerializeField] HPBar townHPBar;
    GameOverPanel gameOverPanel;
    public int score = 0;
    [SerializeField] GameObject AIParent;

    private void Awake()
    {
        townHealth = townHealthMax;
        gameOverPanel = UIContainer.instance.gameOverPanel;
    }

    private void Start()
    {
        SetBar();
    }

    public void DamageTown()
    {
        townHealth--;
        if (townHealth <= 0)
        {
            gameOverPanel.gameObject.SetActive(true);
            AIParent.SetActive(false);
            gameOverPanel.ShowScoreMethod(score);
            if(score > PlayerPrefs.GetInt("highscore") || !PlayerPrefs.HasKey("highscore")) { PlayerPrefs.SetInt("highscore", score); }
            return;
        }
        SetBar();
    }

    void SetBar()
    {
        float per = 1f - (float)townHealth / (float)townHealthMax;
        townHPBar.SetFill(per);
    }

    public void Menu() { SceneManager.LoadScene(0); }
}