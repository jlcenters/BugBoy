using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FollowType
{
    cameraFollowTarget, characterFollowTarget
}
public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float offset;
    [SerializeField] private FollowType followType;



    [Header("New Camera Follow")]
    [SerializeField] private GameObject targetGameObject;
    [SerializeField] private Camera cam;
    [Header("Offset")]
    [SerializeField] private float yAngleMin = -120f;
    [SerializeField] private float yAngleMax = 80f;
    [SerializeField] private float distance = 5f;
    [Header("Mouse Sensitivity")]
    [SerializeField] private float sensitivityX = 4f;
    [SerializeField] private float sensitivityY = 1f;
    [Header("Current Values")]
    [SerializeField] private float currentX = 0f;
    [SerializeField] private float currentY = 0f;



    /*private void Update()
    {
        switch (followType)
        {
            case FollowType.cameraFollowTarget:
                Vector3 follow = target.position;
                transform.position = new(follow.x, follow.y + offset, follow.z - offset);
                break;
            case FollowType.characterFollowTarget:
                Vector3 destination = target.position;
                transform.position = new(destination.x - transform.position.x, destination.y - transform.position.y, destination.z - transform.position.z);
                break;
        }


    }*/



    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        //store mouse location
        currentX += Input.GetAxis("Mouse X") * sensitivityX;
        currentY += Input.GetAxis("Mouse Y") * sensitivityY;
        //y values will not exceed specified y caps
        currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
    }
    private void LateUpdate()
    {
        //offset object
        Vector3 dir = new(0, 0, -distance);
        //rotation based on current values
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        //camera position set based on current values
        cam.transform.position = targetGameObject.transform.position + rotation * dir;
        //will rotate towards new position
        cam.transform.LookAt(targetGameObject.transform.position);
    }
    
}
