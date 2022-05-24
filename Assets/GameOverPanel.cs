using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] TMP_Text resultText;
    const string resultTextKey = "Days: ";
    [SerializeField] float maxTime = 3f;
    WaitForSeconds waitForSeconds;

    public void ShowScoreMethod(int _score)
    {
        waitForSeconds = new WaitForSeconds(maxTime / (float)_score);
        StartCoroutine(ShowScore(_score));
    }

    IEnumerator ShowScore(int _score)
    {
        for (int i = 0; i <= _score; i++)
        {
            resultText.text = resultTextKey + i;
            yield return waitForSeconds;
        }
    }
}