using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    private enum Mode
    {
        Camera,
        CameraInverted,
        Target,
        TargetInverted
    }


    [SerializeField] private Mode mode = Mode.Camera;
    [SerializeField] private Transform target;

    //update logic but after update
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.Camera:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.CameraInverted:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.Target:
                if(target != null)
                {
                    Vector3 lookAt = target.position;
                    lookAt.y = transform.position.y;
                    transform.LookAt(lookAt);
                }
                break;
            case Mode.TargetInverted:
                if (target != null)
                {
                    Quaternion lookAtInverted = new(transform.rotation.x, target.rotation.y, transform.rotation.z, 0f);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookAtInverted, 0.5f);
                }
                break;
        }
        
    }
}
