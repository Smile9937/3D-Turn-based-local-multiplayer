using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUI : MonoBehaviour
{
    private Camera _camera;

    private void OnEnable()
    {
        EventManager.activeCameraChanged += CameraChanged;
        _camera = CameraManager.GetActiveCamera();
    }
    private void OnDisable()
    {
        EventManager.activeCameraChanged -= CameraChanged;
    }
    private void CameraChanged(Camera camera)
    {
        _camera = camera;

    }
    void Update()
    {
        if(_camera != null)
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }
}