using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int playerScore = 0;
    [SerializeField] private float perfectScore = 1000000f;
    [SerializeField] private float maxScorePerCategory = 200000f;
    [SerializeField] private float perfectTime = 1200f;
    [SerializeField] private GameObject gameUI;

    private void Start()
    {
        GameController.Instance.OnStateChange += Instance_OnStateChange;

        HideUI();
    }



    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        //show game timer
        if (GameController.Instance.IsActiveState(GameStates.GameOver))
        {
            //display text
            ShowUI();
            gameUI.SetActive(false);
            scoreText.text = CalculateScore(GameController.Instance.GetGameTime());
        }
        else
        {
            HideUI();
        }
    }



    private void HideUI()
    {
        gameObject.SetActive(false);
    }
    private void ShowUI()
    {
        gameObject.SetActive(true);
    }



    private string CalculateScore(float timeSpent)
    {
        float timeGradePercentage = 0f;

        //if it took at max the 100% tier to finish game, set percentage to 1 (max score)
        if(timeSpent <= perfectTime)
        {
            timeGradePercentage = 1f;
        }
        else
        {
            timeGradePercentage = perfectTime / timeSpent;
        }
        playerScore = Mathf.CeilToInt((maxScorePerCategory * timeGradePercentage));
        playerScore *= 5;
        return playerScore + " / " + perfectScore;
    }
}
