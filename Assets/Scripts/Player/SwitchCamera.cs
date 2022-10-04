using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;
    [SerializeField] private int _cameraIndex;

    private bool _isActive;
    public void ChangeActiveState(bool active)
    {
        _isActive = active;
        if(!active)
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                _cameras[i].depth = 0;
            }
        }
        else
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                _cameras[i].depth = 0;

                if (_cameraIndex == i)
                {
                    _cameras[i].depth++;
                }
            }
            CameraManager.SetActiveCamera(_cameras[_cameraIndex]);
        }
    }
    public void SwitchCameras(InputAction.CallbackContext context)
    {
        if(!_isActive) return;
        if(context.performed)
        {
            _cameraIndex++;
            _cameraIndex %= _cameras.Length;
            for (int i = 0; i < _cameras.Length; i++)
            {
                _cameras[i].depth = 0;

                if (_cameraIndex == i)
                {
                    _cameras[i].depth++;
                }
            }
            CameraManager.SetActiveCamera(_cameras[_cameraIndex]);
        }
    }
}
