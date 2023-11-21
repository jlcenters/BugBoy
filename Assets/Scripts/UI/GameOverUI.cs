using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/*
 * 
RANKING BREAKDOWN:
    S - 100%
    A - 80-99%
    B - 60-79%
    C - 40-59%
    D - 20-39%
    E - 0-19%

S FOR EACH CATEGORY:
    Enemies Defeated - 20
    Flies Collected - 100
    Lives Remaining - 3
    Time to Complete - 10min
 * 
 */



public enum ScoreRank
{
    S, A, B, C, D, E
}



public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameStates thisState;

    [Header("Max Values")]
    [SerializeField] private float perfectScore = 1000000f;
    [SerializeField] private float maxScorePerCategory = 250000f;
    [SerializeField] private float perfectTime = 600f;
    [SerializeField] private float perfectEnemiesDefeated = 20f;
    [SerializeField] private float perfectFliesCollected = 100f;
    [SerializeField] private float perfectLivesRemaining = 3f;

    [Header("Final Ranks and Scores")]
    [SerializeField] private float playerScore = 0;
    [SerializeField] private string playerScoreString;
    [SerializeField] private ScoreRank playerRank;
    [SerializeField] private ScoreRank enemiesDefeatedRank;
    [SerializeField] private string enemiesDefeatedString;
    [SerializeField] private float enemiesDefeatedScore;
    [SerializeField] private ScoreRank fliesCollectedRank;
    [SerializeField] private string fliesCollectedString;
    [SerializeField] private float fliesCollectedScore;
    [SerializeField] private ScoreRank livesRemainingRank;
    [SerializeField] private string livesRemainingString;
    [SerializeField] private float livesRemainingScore;
    [SerializeField] private ScoreRank timeToCompleteRank;
    [SerializeField] private string timeToCompleteString;
    [SerializeField] private float timeToCompleteScore;

    [Header("UI Objects")]
    [SerializeField] private TextMeshProUGUI enemiesDefeatedText;
    [SerializeField] private TextMeshProUGUI fliesCollectedText;
    [SerializeField] private TextMeshProUGUI livesRemainingText;
    [SerializeField] private TextMeshProUGUI timeToCompleteText;

    [SerializeField] private TextMeshProUGUI playerScoreText;
    [SerializeField] private GameObject menuOptionsObject;



    private void Start()
    {
        GameController.Instance.OnStateChange += Instance_OnStateChange;

        HideUI();
    }
    


    private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        //show correct screen if won or lost
        if (GameController.Instance.IsActiveState(thisState))
        {
            //stop in game ui
            gameUI.SetActive(false);
            //get player total score
            CalculateTotalScore(GameController.Instance.GetGameTime());
            //begin score display
            ShowUI();
            ClearUI();
            StartCoroutine(SetUI());
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



    private void CalculateTotalScore(float timeSpent)
    {
        //if game won, include time to complete in score
        switch (thisState)
        {
            case GameStates.GameOver:
                break;
            case GameStates.WinGame:
                //if it took at max the 100% tier to finish game, set percentage to 1 (max score)
                if (timeSpent <= perfectTime)
                {
                    timeToCompleteScore = 1f;
                }
                else
                {
                    timeToCompleteScore = perfectTime / timeSpent;
                    timeToCompleteRank = CalculateRank(timeToCompleteScore);
                    Debug.Log(timeToCompleteScore + "time");

                }
                playerScore += Mathf.CeilToInt((maxScorePerCategory * timeToCompleteScore));
                timeToCompleteString = "Time Spent: " + timeSpent + " / " + perfectTime + " ... " + timeToCompleteRank; //TODO: set string in mm:ss format
                break;
            default: break;
        }
        //enemies defeated
        enemiesDefeatedScore = (float)(PlayerController.Instance.EnemiesDefeated) / perfectEnemiesDefeated;
        enemiesDefeatedRank = CalculateRank(enemiesDefeatedScore);
        enemiesDefeatedScore = CalculateScore(enemiesDefeatedScore);
        enemiesDefeatedString = "Enemies Defeated: " + enemiesDefeatedScore + " / " + perfectEnemiesDefeated + " ... " + enemiesDefeatedRank;

        //flies collected
        fliesCollectedScore = (float)(PlayerController.Instance.inventory.collectables[CollectableType.Flies]) / perfectFliesCollected;
        fliesCollectedRank = CalculateRank(fliesCollectedScore);
        fliesCollectedScore = CalculateScore(fliesCollectedScore);
        fliesCollectedString = "Flies Collected: " + fliesCollectedScore + " / " + perfectFliesCollected + " ... " + fliesCollectedRank;

        //lives remaining
        livesRemainingScore = (float)(PlayerController.Instance.LivesRemaining) / perfectLivesRemaining;
        livesRemainingRank = CalculateRank(livesRemainingScore);
        livesRemainingScore = CalculateScore(livesRemainingScore);
        livesRemainingString = "Lives Remaining: " + livesRemainingScore + " / " + perfectLivesRemaining + " ... " + livesRemainingRank;

        //player total score
        playerRank = CalculateRank(playerScore / perfectScore);
        playerScoreString = "Total: " + playerScore + " / " + perfectScore + " ... " + playerRank;
        Debug.Log(playerScore + "total" + " and " + playerRank + " rank");


    }

    private ScoreRank CalculateRank(float percentage)
    {
        if (percentage <= 0.19f)
        {
            return ScoreRank.E;
        }
        else if (percentage <= 0.39f)
        {
            return ScoreRank.D;
        }
        else if (percentage <= 0.59f)
        {
            return ScoreRank.C;
        }
        else if (percentage <= 0.79f)
        {
            return ScoreRank.B;
        }
        else if (percentage <= 0.99f)
        {
            return ScoreRank.A;
        }
        else
        {
            return ScoreRank.S;
        }
    }
    //cannot use on time score because it references a maximum value
    private float CalculateScore(float percentage)
    {
        //increase player score
        playerScore += Mathf.CeilToInt(maxScorePerCategory * percentage);
        return percentage;
    }

    private void ClearUI()
    {
        enemiesDefeatedText.text = "";
        fliesCollectedText.text = "";
        livesRemainingText.text = "";
        timeToCompleteText.text = "";
        playerScoreText.text = "";
        menuOptionsObject.SetActive(false);
    }
    private IEnumerator SetUI()
    {
        //set new values, with a slight pause between each value
        enemiesDefeatedText.text = enemiesDefeatedString;
        yield return new WaitForSeconds(0.25f);
        fliesCollectedText.text = fliesCollectedString;
        yield return new WaitForSeconds(0.25f);
        livesRemainingText.text = livesRemainingString;
        yield return new WaitForSeconds(0.25f);

        //only set time score if game was won
        if(thisState == GameStates.WinGame)
        {
            timeToCompleteText.text = timeToCompleteString;
        }
        else
        {
            timeToCompleteText.gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(0.5f);
        playerScoreText.text = playerScoreString;
        yield return new WaitForSeconds(1.5f);

        //activate menu options
        menuOptionsObject.SetActive(true);

        yield return null;
    }
}
