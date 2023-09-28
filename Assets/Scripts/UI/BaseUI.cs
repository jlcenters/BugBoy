using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseUI : MonoBehaviour
{
    [SerializeField] private GameStates gameStateImplemented;

    private void Start()
    {
        GameController.Instance.OnStateChange += GameController_OnStateChange;
        HideUI();
    }



    private void GameController_OnStateChange(object sender, System.EventArgs e)
    {
        if (GameController.Instance.IsActiveState(gameStateImplemented))
        {
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
    }
}
