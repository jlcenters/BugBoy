using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;



    /*private void Start()
    {
        GameController.Instance.OnStateChange += Instance_OnStateChange;
        HideUI();
    }*/
    private void Update()
    {
        timerText.text = CalculateTimer(GameController.Instance.GetGameTimer());
    }



    /*private void Instance_OnStateChange(object sender, System.EventArgs e)
    {
        //show game timer
        if (GameController.Instance.IsActiveState(GameStates.GamePause))
        {
            //display text
            ShowUI();
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
    }*/

    private string CalculateTimer(float time)
    {
        int secondsRemaining = (int)time;
        int hours = secondsRemaining / 3600;
        secondsRemaining %= 3600;
        int minutes = secondsRemaining / 60;
        secondsRemaining %= 60;
        
        //return "" + hours + ":" + minutes + ":" + secondsRemaining;
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, secondsRemaining);
    }
}
