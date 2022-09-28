using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateUnitUI : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        EventManager.Instance.setActiveCamera += SetActiveCamera;
    }
    private void OnDisable()
    {
        EventManager.Instance.setActiveCamera -= SetActiveCamera;
    }
    void Update()
    {
        transform.LookAt(transform.position + _camera.transform.rotation * Vector3.forward, _camera.transform.rotation * Vector3.up);
    }

    public void SetActiveCamera(Camera camera)
    {
        _camera = camera;
    }
}
