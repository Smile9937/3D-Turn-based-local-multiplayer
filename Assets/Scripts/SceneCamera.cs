using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class SceneCamera : MonoBehaviour
{
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>(); 
    }
    private void GetCamera()
    {
        CameraManager.AddToList(_camera);
    }
    private void OnEnable()
    {
        EventManager.getCameras += GetCamera;
    }
    private void OnDisable()
    {
        EventManager.getCameras -= GetCamera;
    }
}