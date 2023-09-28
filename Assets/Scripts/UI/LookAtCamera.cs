using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        Normal,
        Inverted,
        CameraForward,
        CameraForwardInverted
    }


    [SerializeField] private Mode mode = Mode.Normal;

    //update logic but after update
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.Normal:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;
            case Mode.Inverted:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }
        
    }
}
