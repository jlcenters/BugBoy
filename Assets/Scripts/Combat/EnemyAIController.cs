using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAIController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private float waitTime = 4f;
    private float timeToRotate = 2f;
    [SerializeField] private float walkingSpeed = 6f;
    [SerializeField] private float runningSpeed = 9f;

    [SerializeField] private float viewRadius = 15f;
    [SerializeField] private float viewAngle = 90f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private float meshResolution = 1f;
    [SerializeField] private int edgeIterations = 4;
    [SerializeField] private float edgeDistance = 0.5f;

    [SerializeField] private Transform[] waypoints;
    private int currentWaypoint = 0;
    
    private Vector3 playerLastPosition = Vector3.zero;
    private Vector3 playerCurrentPosition = Vector3.zero;

    private float timeToWait;
    private float timeToRotate_m;
    private bool playerInRange = false;
    private bool playerNear = false;
    private bool isPatroling;
    private bool caughtPlayer = false;



    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = walkingSpeed;
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);

        timeToWait = waitTime;
        timeToRotate_m = timeToRotate;
    }



    private void Patrol()
    {
        if(playerNear)
        {
            if(timeToRotate_m <= 0)
            {
                ToggleMovement(true, walkingSpeed);
                LookingForPlayer(playerLastPosition);
            }
            else
            {
                ToggleMovement(false);
                timeToRotate_m -= Time.deltaTime;
            }
        }
        else
        {
            playerNear = false;
            playerLastPosition = Vector3.zero;
        }

    }
    private void ToggleMovement(bool isMoving, float moveSpeed=0f)
    {
        navMeshAgent.isStopped = !isMoving;
        navMeshAgent.speed = moveSpeed;
    }
    public void NextWaypoint()
    {
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
    }
    private void CaughtPlayer()
    {
        caughtPlayer = true;
    }
    private void LookingForPlayer(Vector3 player)
    {
        navMeshAgent.SetDestination(player);
        if(Vector3.Distance(transform.position, player) <= 0.3)
        {
            if(timeToWait <= 0)
            {
                playerNear = false;
                ToggleMovement(true, walkingSpeed);
                navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
                timeToWait = waitTime;
                timeToRotate_m = timeToRotate;
            }
            else
            {
                ToggleMovement(false);
                timeToWait -= Time.deltaTime;
            }
        }
    }
    private void EnvironmentView()
    {
        Collider[] playerCollider = Physics.OverlapSphere(transform.position, viewRadius, playerLayer);
        for(int i = 0; i < playerCollider.Length; i++)
        {
            Transform playerTransform = playerCollider[i].transform;
            Vector3 directionToPlayer = (playerCollider[i].transform.position - transform.position).normalized;
            
            if(Vector3.Angle(transform.forward, directionToPlayer) < viewAngle / 2)
            {
                float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
                
                if(!Physics.Raycast(transform.position, directionToPlayer, playerDistance, obstacleLayer))
                {
                    playerInRange = true;
                    isPatroling = false;
                }
                else
                {
                    playerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, playerTransform.position) > viewRadius)
            {
                playerInRange = false;
            }
            if (playerInRange)
            {
                playerCurrentPosition = playerTransform.position;
            }
        }

    }
}
