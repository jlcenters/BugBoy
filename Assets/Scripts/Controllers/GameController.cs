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
    public static GameController Instance { get; private set; }

    public event EventHandler OnStateChange;

    private GameStates state;
    [SerializeField] private float waitingTimer = 1f;
    [SerializeField] private float gameTimer = 0f;
    private float gameTime = 0f;
    private bool gamePaused = false;
    private bool inDialogue = false;



    private void Awake()
    {
        state = GameStates.WaitingToStart;

        //if instance is taken and it is not self, destroy self; else, instantiate instance as self
        if(Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        InputController.Instance.OnPause += GameInput_OnPause;

        DialogueController.Instance.OnDialogue += DialogueController_OnDialogue;
    }
    private void OnDestroy()
    {
        InputController.Instance.OnPause -= GameInput_OnPause;
        
        DialogueController.Instance.OnDialogue -= DialogueController_OnDialogue;
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
    private void DialogueController_OnDialogue()
    {
        ToggleDialogue();
    }
    public void ToggleDialogue()
    {
        inDialogue = !inDialogue;
        if(inDialogue)
        {
            state = GameStates.InDialogue;
            gameTime = gameTimer;
        }
        else
        {
            state = GameStates.GamePlaying;
            gameTimer = gameTime;
            OnStateChange?.Invoke(state, EventArgs.Empty);
        }
    }
}
