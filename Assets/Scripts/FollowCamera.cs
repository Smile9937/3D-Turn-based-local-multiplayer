using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Vector3 _offset;
    private GameObject _target;
    public void SetTarget(GameObject target, Vector3 offset)
    {
        _target = target;
        _offset = offset;
    }

    private void Update()
    {
        transform.position = _target.transform.position + _offset;
    }
}