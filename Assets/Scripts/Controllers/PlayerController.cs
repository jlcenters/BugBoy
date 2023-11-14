using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;



public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance {  get; private set; }
    [SerializeField] private InputController inputController;
    public PlayerUI playerUi;
    public Inventory inventory;



    //TODO: active hat; active item; 
    [Header("Combat")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int hp;
    [SerializeField] private int maxHp;
    [SerializeField] private int attackPower;
    [SerializeField] private float attackSpeed;
    private bool canAttack = true;

    [Header("Walking")]
    [SerializeField] private float rotateSpeed = 10f;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float playerRadius = 0.7f;
    [SerializeField] private float playerHeight = 2f;
    [SerializeField] private float moveDistance;
    private bool canMove;

    [Header("Jumping")]
    [SerializeField] private float jumpForce = 1f;
    [SerializeField] private float jumpMultiplier = 1f;
    [SerializeField] private float fallMultiplier = 1f;
    public ForceMode ForceMode;
    //can jump bool

    [Header("Interactions")]
    [SerializeField] private LayerMask interactablesLayer;
    [SerializeField] private float interactDistance = 2f;
    private Vector3 lastInteractDirection;

    private Rigidbody rb;

    public int Hp => hp;
    public int MaxHp => maxHp;



    private void Awake()
    {
        lastInteractDirection = new(0f, 0f, 0f);
        inventory = GetComponent<Inventory>();
        inputController = GetComponent<InputController>();
        rb = GetComponent<Rigidbody>();
        ResetHp();

        Instance = this;
        
    }
    private void Start()
    {
        inputController.OnInteract += InputController_OnInteract;
        inputController.OnJump += InputController_OnJump;
        inputController.OnAttack += InputController_OnAttack;
        inputController.OnUseItem += InputController_OnUseItem;
    }
    private void Update()
    {
        //movement logic
        if(inventory.activeHat == HatType.Ant)
        {
            moveDistance = (moveSpeed + inventory.ANT_BUFF) * Time.deltaTime;
        }
        else
        {
            moveDistance = moveSpeed * Time.deltaTime;
        }
        Vector2 inputDir = inputController.GetMovementNormalized();

        FacingDirection(inputDir.x, inputDir.y);
        if(GameController.Instance.IsActiveState(GameStates.GamePlaying))
        {
            Move(inputDir.x, inputDir.y);
        }

        //if falling, apply fall multiplier
        if (IsFalling())
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (!IsFalling())
        {
            rb.velocity += Vector3.up * Physics.gravity.y * (jumpMultiplier - 1) * Time.deltaTime;
        }

    }



    //Movement Methods
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
        //if game paused, do not attempt interaction
        if(GameController.Instance.IsActiveState(GameStates.GamePause))
        {
            return;
        }
        //else if in dialogue, wait to see if finished typing
        else if(GameController.Instance.IsActiveState(GameStates.InDialogue))
        {
            if(DialogueController.Instance.IsTyping)
            {
                return;
            }
            else
            {
                DialogueController.Instance.TryNextLine();
            }
        }
        //else, attempt to interact w an interactable
        else
        {
            
            if (IsInLayerVicinity(interactablesLayer))
            {
                if (CheckLayerDistance(interactablesLayer).transform.TryGetComponent(out IInteractable interactable))
                {
                    interactable.Interact(GetComponent<PlayerController>());
                }
            }
        }
    }
    private void InputController_OnJump(object sender, System.EventArgs e)
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode);
    }
    private void InputController_OnAttack(object sender, System.EventArgs e)
    {
        Debug.Log("attack input selected");
        //if not in distance of an enemy, return
        if (!IsInLayerVicinity(enemyLayer))
        {
            return;
        }
        //if paused, return
        else if (GameController.Instance.IsActiveState(GameStates.GamePause))
        {
            return;
        }
        //if in dialogue, return
        else if (GameController.Instance.IsActiveState(GameStates.InDialogue))
        {
            return;
        }
        //if already attacking/attack timer hasn't reset yet, return
        if (!canAttack)
        {
            return;
        }
        else
        {
            StartCoroutine(Attack() );
        }
    }
    private void InputController_OnUseItem(object sender, System.EventArgs e)
    {
        //TODO: implement mmb to choose item to use before using

        //currently only using and receiving honey
        //check if inventory use item method will return true
        bool consumedItem = inventory.ConsumeTool(ItemType.Honey, 1);
        if (consumedItem)
        {
            //if so, it was successfully consumed
            Debug.Log("could use item");
            //update new item ui
            playerUi.SetItem();
            //heal player up to max amt
            ResetHp();
            //update hp ui
            playerUi.SetHp();
        }
        else
        {
            //otherwise, do nothing         
            Debug.Log("could not use item");
        }
    }
    private bool IsInLayerVicinity(LayerMask layer)
    {
        Vector2 inputDir = inputController.GetMovementNormalized();
        Vector3 faceDirection = new(inputDir.x, 0f, inputDir.y);

        if (faceDirection != Vector3.zero)
        {
            lastInteractDirection = faceDirection;
        }

        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, layer))
        {
            return true;
        }
        return false;
    }
    private RaycastHit CheckLayerDistance(LayerMask layer)
    {
        Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit hit, interactDistance, layer);
        return hit;
    }
    private void FacingDirection(float x, float z)
    {
        Vector3 faceDirection = new(x, 0f, z);

        if(faceDirection != Vector3.zero)
        {
            lastInteractDirection = faceDirection;
        }
    }
    private bool IsFalling()
    {
        return rb.velocity.y <= 0;
    }



    //Combat Methods
    private void ResetHp()
    {
        hp = maxHp;
    }
    public void TakeDamage(int damage)
    {
        hp -= damage;

        if(hp <= 0)
        {
            hp = 0;
            GameController.Instance.PlayerFainted();
            Debug.Log("I Dieded");
        }

        StartCoroutine(playerUi.UpdateHp());
    }
    public IEnumerator Attack()
    {
        canAttack = false;
        Debug.Log("attacking enemy");
        if (CheckLayerDistance(enemyLayer).transform.TryGetComponent(out IAttackable attackable))
        {
            attackable.ReceiveDamage(attackPower);
        }

        Debug.Log("waiting to attack again");
        yield return new WaitForSeconds(attackSpeed);
        Debug.Log("can attack again");
        canAttack = true;
    }
}
