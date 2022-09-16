using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private Camera[] _cameras;
    private int _cameraIndex;
    
    public void SwitchCameras(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            for (int i = 0; i < _cameras.Length; i++)
            {
                _cameras[i].depth = 0;

                if (_cameraIndex == i)
                {
                    _cameras[i].depth++;
                }
            }
            _cameraIndex++;
            _cameraIndex %= _cameras.Length;
        }
    }
}
