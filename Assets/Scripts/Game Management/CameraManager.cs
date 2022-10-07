using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    private static Camera _activeCamera;
    private static List<Camera> _sceneCameras = new List<Camera>();

    public static void AddToList(Camera sceneCamera)
    {
        _sceneCameras.Add(sceneCamera);
    }
    public static void RemoveFromList(Camera sceneCamera)
    {
        _sceneCameras.Remove(sceneCamera);
    }
    public static void EmptyList()
    {
        _sceneCameras.Clear();
    }
    public static void SetCameras()
    {
        EmptyList();
        EventManager.InvokeGetCameras();
    }
    public static void SetActiveCamera(Camera camera)
    {
        _activeCamera = camera;
        for(int i = 0; i < _sceneCameras.Count; i++)
        {
            _sceneCameras[i].gameObject.SetActive(_sceneCameras[i] == _activeCamera);
        }
        EventManager.InvokeActiveCameraChanged(_activeCamera);
    }
    public static Camera GetActiveCamera()
    {
        return _activeCamera;
    }
}