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


    private void Update()
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


    }

}
