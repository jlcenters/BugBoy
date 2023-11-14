using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public enum InputBindings 
{
    Up,
    Left,
    Down,
    Right,
    Jump,
    Interact,
    ToggleHW,
    Attack,
    UseItem,
    Ability
}



public class InputController : MonoBehaviour
{
    private const string PLAYER_PREFS_BINDINGS = "InputBindings";
    public static InputController Instance {  get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteract;
    public event EventHandler OnJump;
    public event EventHandler OnPause;
    public event EventHandler OnAttack;
    public event EventHandler OnUseItem;

    private void Awake()
    {
            Instance = this;
       

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //load input bindings
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }

        //subscribe
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
        playerInputActions.Player.Attack.performed += Attack_performed;
        playerInputActions.Player.UseItem.performed += UseItem_performed;
    }
    private void OnDestroy()
    {
        //unsubscribe
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Jump.performed -= Jump_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;
        playerInputActions.Player.Attack.performed -= Attack_performed;
        playerInputActions.Player.UseItem.performed -= UseItem_performed;

        playerInputActions.Dispose();
    }



    public Vector2 GetMovementNormalized()
    {
        Vector2 inputDir = playerInputActions.Player.Move.ReadValue<Vector2>();

        //they will both increase with a magnitude of 1
        return inputDir.normalized;
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }
    public void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnJump?.Invoke(this, EventArgs.Empty);
    }
    private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }
    private void Attack_performed(InputAction.CallbackContext obj)
    {
        OnAttack?.Invoke(this, EventArgs.Empty);
    }
    private void UseItem_performed(InputAction.CallbackContext obj)
    {
        OnUseItem?.Invoke(this, EventArgs.Empty);
    }



    public string GetBindingText(InputBindings binding)
    {
        switch (binding)
        {
            default:
            case InputBindings.Left:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case InputBindings.Right:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case InputBindings.Up:
                return playerInputActions.Player.Move.bindings[1].ToDisplayString();
            case InputBindings.Down:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();
            case InputBindings.Jump:
                return playerInputActions.Player.Jump.bindings[0].ToDisplayString();
            case InputBindings.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case InputBindings.ToggleHW:
                return playerInputActions.Player.ToggleHatWheel.bindings[0].ToDisplayString();
            case InputBindings.Attack:
                return playerInputActions.Player.Attack.bindings[0].ToDisplayString();
            case InputBindings.UseItem:
                return playerInputActions.Player.UseItem.bindings[0].ToDisplayString();
        }
    }
    public void RebindBinding(InputBindings binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case InputBindings.Up:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 1;
                break;
            case InputBindings.Down:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 2;
                break;
            case InputBindings.Left:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 3;
                break;
            case InputBindings.Right:
                inputAction = playerInputActions.Player.Move;
                bindingIndex = 4;
                break;
            case InputBindings.Jump:
                inputAction = playerInputActions.Player.Jump;
                bindingIndex = 0;
                break;
            case InputBindings.Interact:
                inputAction = playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case InputBindings.ToggleHW:
                inputAction = playerInputActions.Player.ToggleHatWheel;
                bindingIndex = 0;
                break;
            case InputBindings.Attack:
                inputAction = playerInputActions.Player.Attack;
                bindingIndex = 0;
                break;
            case InputBindings.UseItem:
                inputAction = playerInputActions.Player.UseItem;
                bindingIndex = 0;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
        }).Start();
    }
}
