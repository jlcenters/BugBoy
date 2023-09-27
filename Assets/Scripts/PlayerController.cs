using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float drag = 1f;
    public ForceMode ForceMode;
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float moveDistance;
    [SerializeField] private float interactDistance = 2f;

    private Vector3 lastInteractDirection;
    private Rigidbody rb;
    private bool canMove;

    public Inventory inventory;
    [SerializeField] private InputController inputController;
    [SerializeField] private LayerMask interactablesLayer;



    private void Awake()
    {
        lastInteractDirection = new(0f, 0f, 0f);
        inventory = GetComponent<Inventory>();
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        inputController.OnInteract += InputController_OnInteract;
        inputController.OnJump += InputController_OnJump;
    }
    private void Update()
    {
        //movement logic
        moveDistance = moveSpeed * Time.deltaTime;
        Vector2 inputDir = inputController.GetMovementNormalized();

        FacingDirection(inputDir.x, inputDir.y);
        Move(inputDir.x, inputDir.y);

    }



    private void Move(float x, float z)
    {
        Vector3 moveDirection = new(x, 0f, z);
        canMove = CheckMove(moveDirection);

        if (!canMove)
        {
            //check which axis you can move on
            Vector3 moveX = new Vector3(moveDirection.x, 0f, 0f).normalized;
            Vector3 moveZ = new Vector3(0, 0, moveDirection.z).normalized;
            if (CheckMove(moveX))
            {
                moveDirection = moveX;
                canMove = true;
            }
            else if(CheckMove(moveZ))
            {
                moveDirection = moveZ;
                canMove = true;
            }
        }
        if (canMove)
        {
            transform.position += moveDistance * moveDirection;
        }
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

    }
    private bool CheckMove(Vector3 direction)
    {

        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, direction, moveDistance);

    }



    private void InputController_OnInteract(object sender, System.EventArgs e)
    {
        Vector2 inputDir = inputController.GetMovementNormalized();
        Vector3 faceDirection = new(inputDir.x, 0f, inputDir.y);

        if (faceDirection != Vector3.zero)
        {
            lastInteractDirection = faceDirection;
        }


        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, interactablesLayer))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact(GetComponent<PlayerController>());
            }
        }
    }
    private void InputController_OnJump(object sender, System.EventArgs e)
    {
        rb.AddForce(Vector3.up * drag, ForceMode);
    }
    private void FacingDirection(float x, float z)
    {
        Vector3 faceDirection = new(x, 0f, z);

        if(faceDirection != Vector3.zero)
        {
            lastInteractDirection = faceDirection;
        }
    }
}
