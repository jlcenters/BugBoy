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
    UseItem
}



public class InputController : MonoBehaviour
{
    public static InputController Instance {  get; private set; }

    private PlayerInputActions playerInputActions;

    public event EventHandler OnInteract;
    public event EventHandler OnJump;
    public event EventHandler OnPause;

    private void Awake()
    {
        Instance = this;

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //subscribe
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
    }
    private void OnDestroy()
    {
        //unsubscribe
        playerInputActions.Player.Interact.performed -= Interact_performed;
        playerInputActions.Player.Jump.performed -= Jump_performed;
        playerInputActions.Player.Pause.performed -= Pause_performed;

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
    public string RebindBinding(InputBindings binding, Action onActionRebound)
    {
        playerInputActions.Player.Disable();
        playerInputActions.Player.Move.PerformInteractiveRebinding(1).OnComplete(callback =>
        {
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
        }).Start();
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
}