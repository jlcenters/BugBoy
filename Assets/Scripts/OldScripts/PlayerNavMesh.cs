using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * 
 * 
 * 
 * SCRIPT OBSOLETE
 * 
 * 
 * 
 */
public class PlayerNavMesh : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform movePositionTransform;



    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    private void Update()
    {
        agent.destination = movePositionTransform.position;
    }
}
