using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button confirmQuitButton;
    [SerializeField] private Button goBackButton;

    [SerializeField] private GameObject confirmQuitMenu;

    private bool quittingGame = false;
    private bool mainMenu = false;



    private void Awake()
    {
        resumeButton.onClick.AddListener(() =>
        {
            GameController.Instance.TogglePauseMenu();
        }); 
        mainMenuButton.onClick.AddListener(() =>
        {
            mainMenu = true;
            ToggleConfirmMenu(true);
        }); 
        quitButton.onClick.AddListener(() =>
        {
            quittingGame = true;
            ToggleConfirmMenu(true);
        }); 
        confirmQuitButton.onClick.AddListener(() =>
        {
            if (quittingGame)
            {
                Application.Quit();
            }
            else if(mainMenu)
            {
                GameController.Instance.TogglePauseMenu();
                Loader.Load(Scenes.MainMenu);
            }
        }); 
        goBackButton.onClick.AddListener(() =>
        {
            mainMenu = false;
            quittingGame = false;
            ToggleConfirmMenu(false);
        });
    }



    private void ToggleConfirmMenu(bool isOn)
    {
        confirmQuitMenu.SetActive(isOn);
    }
}
