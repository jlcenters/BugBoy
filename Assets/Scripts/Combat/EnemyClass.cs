using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class EnemyClass : MonoBehaviour
{
    [SerializeField] private Vector3 startingPosition;



    private void Start()
    {
        startingPosition = transform.position;
    }
    private void Update()
    {
        transform.position = GetRoamingPosition();
    }



    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDir() * Random.Range(10f, 70f);
    }
    private Vector3 GetRandomDir()
    {
        return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
