using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;




public class GameTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;



    private void Update()
    {
        timerText.text = CalculateTimer(GameController.Instance.GetGameTimer());
    }



    private string CalculateTimer(float time)
    {
        int secondsRemaining = (int)time;
        int hours = secondsRemaining / 3600;
        secondsRemaining %= 3600;
        int minutes = secondsRemaining / 60;
        secondsRemaining %= 60;
        
        return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, secondsRemaining);
    }
}
