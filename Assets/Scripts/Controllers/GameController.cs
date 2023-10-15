using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public enum GameStates
{
    WaitingToStart,
    GamePlaying,
    GamePause,
    GameOver,
    InDialogue
}



public class GameController : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    public static GameController Instance { get; private set; }

    public event EventHandler OnStateChange;

    private GameStates state;
    [SerializeField] private float waitingTimer = 1f;
    [SerializeField] private float gameTimer = 0f;
    private float gameTime = 0f;
    private bool gamePaused = false;



    private void Awake()
    {
        state = GameStates.WaitingToStart;
        Instance = this;
    }
    private void Start()
    {
        InputController.Instance.OnPause += GameInput_OnPause;

        dialogueController.OnShowDialogue += DialogueController_OnShowDialogue;
        dialogueController.OnCloseDialogue += DialogueController_OnCloseDialogue;
    }
    private void Update()
    {
        switch (state)
        {
            case GameStates.WaitingToStart:
                waitingTimer -= Time.deltaTime;
                if (waitingTimer < 0f)
                {
                    state = GameStates.GamePlaying;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }
                break;
            case GameStates.GamePlaying:
                gameTimer += Time.deltaTime;
                /*if(gameTimer > 101f)
                {
                    gameTime = gameTimer;
                    state = GameStates.GameOver;
                    OnStateChange?.Invoke(this, EventArgs.Empty);
                }*/
                break;
            case GameStates.GamePause:
                OnStateChange?.Invoke(state, EventArgs.Empty);
                break;
            case GameStates.InDialogue:
                OnStateChange?.Invoke(state, EventArgs.Empty);
                break;
            case GameStates.GameOver:
                //gameTime = gameTimer;
                break;
        }
    }



    public bool IsActiveState(GameStates desiredState)
    {
        return state == desiredState;
    }
    public float GetGameTimer()
    {
        return gameTimer;
    }
    public float GetGameTime()
    {
        return gameTime;
    }
    private void GameInput_OnPause(object sender, EventArgs e)
    {
        TogglePauseMenu();
    }
    public void TogglePauseMenu()
    {
        gamePaused = !gamePaused;
        if(gamePaused)
        {
            state = GameStates.GamePause;
            Time.timeScale = 0f;
        }
        else
        {
            state = GameStates.GamePlaying;
            Time.timeScale = 1f;
            OnStateChange?.Invoke(state, EventArgs.Empty);
        }
    }
    private void DialogueController_OnCloseDialogue()
    {
        throw new NotImplementedException();
    }
    private void DialogueController_OnShowDialogue()
    {
        throw new NotImplementedException();
    }
}
