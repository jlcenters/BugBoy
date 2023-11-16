using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private void Update()
    {
        this.transform.position = InputController.Instance.UpdateCameraPosition();
    }
}
