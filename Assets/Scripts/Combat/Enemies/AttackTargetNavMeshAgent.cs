/*using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;*/
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;



//TODO: break code down by behavior
//TODO: implement states
public enum AgentState
{
    Patroling, Chasing, Attacking
}



public class AttackTargetNavMeshAgent : MonoBehaviour
{
    [Header("Agent Info")]
    [SerializeField] private float waitTime = 4f;
    [SerializeField] private float rotateTime = 2f;
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float runningSpeed = 9f;
    [SerializeField] private float stopDistance = 1.0f;
    [SerializeField] private float pauseChaseDistance = 2.5f;
    [SerializeField] private float giveUpChaseDistance = 6f;

    [Header("Targeting Info")]
    [SerializeField] private float viewRadius = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private LayerMask obstacleLayer;

    //waypoints for patroling
    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint = 0;

    //player location references
    private Vector3 lastPlayerPosition = Vector3.zero;
    private Vector3 playerCurrentPosition = Vector3.zero;

    //manipulatable stats
    private float timeToWait;
    private float timeToRotate;
    //TODO: instead of bools, use as states
    private bool targetInRange = false;
    private bool targetNear = false;
    private bool isPatroling = true;
    private bool caughtTarget = false;
    private bool canAttackTarget = true;

    //component references
    private NavMeshAgent agent;
    private Enemy enemyScript;

    /* NOT USED
    [SerializeField] private float meshResolution = 1f;
    [SerializeField] private int edgeIterations = 4;
    [SerializeField] private float edgeDistance = 0.5f;
        private Rigidbody rb;
    */



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        enemyScript = GetComponent<Enemy>();
    }
    private void Start()
    {
        agent.isStopped = false;
        agent.speed = walkingSpeed;
        agent.stoppingDistance = stopDistance;
        SetDestinationFromList();

        timeToWait = waitTime;
        timeToRotate = rotateTime;
    }
    private void Update()
    {
        //TODO: when player caught, attack
        SearchForTarget();

        if (caughtTarget)
        {
            if (enemyScript != null && canAttackTarget)
            {
                StartCoroutine(Attack());
            }
        }

        if (!isPatroling)
        {
            Chase();
        }
        else
        {
            Patrol();
        }
        
    }



    private void Patrol()
    {
        //if target near, walk towards it; else, go on to next waypoint
        if(targetNear)
        {
            //if rotation completed, walk towards target; else, continue rotation
            if(timeToRotate <= 0)
            {
                ToggleMovement(true, walkingSpeed);
                LookAtTarget(lastPlayerPosition);
            }
            else
            {
                ToggleMovement(false);
                timeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            lastPlayerPosition = Vector3.zero;
            targetNear = false;
            SetDestinationFromList();

            //if stopping distance met, check wait time
            if(agent.remainingDistance <= agent.stoppingDistance)
            {
                //if wait time reached, begin walking to next patrol point; else, continue waiting
                if(timeToWait <= 0)
                {
                    NextWaypoint();
                    ToggleMovement(true, walkingSpeed);
                    timeToWait = waitTime;
                }
                else
                {
                    ToggleMovement(false);
                    timeToWait -= Time.deltaTime;
                }
            }
        }
    }
    private void Chase()
    {
        //reset
        targetNear = false;
        lastPlayerPosition = Vector3.zero;

        //if player was not caught, run and set destination to where the player is
        if(!caughtTarget)
        {
            ToggleMovement(true, runningSpeed);
            agent.SetDestination(playerCurrentPosition);
        }

        //if agent got to stopping distance, check if player will stop chasing
        if(agent.remainingDistance <= agent.stoppingDistance)
        {
            float targetDist = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag(targetTag).transform.position);

            //if player is out of view and the wait time has been met, return to patroling;
            //else if player is 2.5 units from the enemy, stop chasing and begin wait counter
            if (!caughtTarget)
            {
                Debug.Log("chasing");

                if (targetDist < pauseChaseDistance)
                {
                    SetTargetCaught(true);
                }
                else if (timeToWait <= 0 && targetDist >= giveUpChaseDistance)
                {
                    Debug.Log("back to patroling");
                    isPatroling = true;
                    targetNear = false;
                    ToggleMovement(true, walkingSpeed);
                    timeToRotate = rotateTime;
                    timeToWait = waitTime;
                    SetDestinationFromList();
                }
            }
            if(targetDist >= pauseChaseDistance)
            {
                
                    Debug.Log("attempting to catch again");
                    SetTargetCaught(false);
                    targetNear = true;
                    ToggleMovement(false);
                    timeToWait -= Time.deltaTime;
                
            }
        }
    }
    private IEnumerator Attack()
    {
        canAttackTarget = false;
        Debug.Log("attacking");
        enemyScript.DamagePlayer(PlayerController.Instance);
        yield return new WaitForSeconds(enemyScript.AttackSpeed);
        canAttackTarget = true;
        //when distance between player and enemy == 0; immediate attack
        //toggle movement
        //get physics overlap sphere similar to search for target
        //if player in sphere, invoke damage player event at a rate of enemySpeed per second; using enemyPower as a multiplier
        //if player leaves sphere, chase
    }
    private void ToggleMovement(bool isMoving, float moveSpeed=0f)
    {
        agent.isStopped = !isMoving;
        agent.speed = moveSpeed;
    }
    public void NextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        SetDestinationFromList();
    }
    private void SetTargetCaught(bool isCaught)
    {
        caughtTarget = isCaught;
    }
    private void LookAtTarget(Vector3 lastPlayerPosition)
    {
        agent.SetDestination(lastPlayerPosition);
        //if last player position is reached, check if wait time reached
        if(Vector3.Distance(transform.position, lastPlayerPosition) <= 0.3)
        {
            //if wait time is over, return to moving and reset time stats; else, continue waiting
            if(timeToWait <= 0)
            {
                targetNear = false;
                ToggleMovement(true, walkingSpeed);
                SetDestinationFromList();
                timeToWait = waitTime;
                timeToRotate = rotateTime;
            }
            else
            {
                ToggleMovement(false);
                timeToWait -= Time.deltaTime;
            }
        }
    }
    private void SearchForTarget()
    {
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, viewRadius, targetLayer);
        
        for(int i = 0; i < playerCollider.Length; i++)
        {
            //set transform, direction, and distance from to current
            Transform playerTransform = playerCollider[i].transform;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);

            //if the player is within viewpoint, check distance
            if (Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                //if there is an obstacle in the way, agent cannot see target but knows target is in range; else, target not in range
                if(!Physics.Raycast(transform.position, directionToPlayer, playerDistance, obstacleLayer))
                {
                    targetInRange = true;
                    isPatroling = false;
                }
                else
                {
                    targetInRange = false;
                }
            }

            //if target is farther than view radius, out of range
            if (playerDistance > viewRadius)
            {
                targetInRange = false;
            }
            //if target in range, set position variable to current
            if (targetInRange)
            {
                playerCurrentPosition = playerTransform.position;
                targetNear = true;
            }
        }
    }
    private bool SetDestinationFromList()
    {
        return agent.SetDestination(waypoints[currentWaypoint].position);
    }
}
