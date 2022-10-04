using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    private Camera _camera;
    void Update()
    {
        _camera = CameraManager.GetActiveCamera();
        if(_camera != null)
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}