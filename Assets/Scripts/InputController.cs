using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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

        //check for interactions and jumps
        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.Jump.performed += Jump_performed;
        playerInputActions.Player.Pause.performed += Pause_performed;
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
}
