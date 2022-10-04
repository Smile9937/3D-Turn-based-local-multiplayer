using UnityEngine;

public static class CameraManager
{
    private static Camera _activeCamera;
    private static FollowCamera _followCamera;
    public static void SetActiveCamera(Camera camera)
    {
        _activeCamera = camera;
    }

    public static Camera GetActiveCamera()
    {
        return _activeCamera;
    }

    public static void StartFollowCamera(GameObject gameObject)
    {
        if(_followCamera == null)
        {
            _followCamera = new FollowCamera();
            _followCamera.name = "Follow Camera";
        }
        _followCamera.SetTarget(gameObject, new Vector3(0, 3, 3));
    }
}