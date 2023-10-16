using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offset;


    private void Update()
    {
        Vector3 follow = target.position;
        transform.position = new(follow.x, follow.y + offset, follow.z - offset);

        //transform.forward = new(target.forward.x, transform.forward.y, transform.forward.z);
    }
}
