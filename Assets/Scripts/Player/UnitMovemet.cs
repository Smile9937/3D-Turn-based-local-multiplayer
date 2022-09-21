using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class UnitMovemet : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _movementSpeed = 0.3f;
    [SerializeField] private float _jumpHeight = 6f;
    private Rigidbody _rigidbody;
    private Vector2 _moveValue;
    private Collider _collider;
    private Vector2 _mousePos;
    private bool _isActivePlayer;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }

    public void ChangeActiveState(bool isActive)
    {
        _isActivePlayer = isActive;
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(!_isActivePlayer) return;
        _moveValue = context.ReadValue<Vector2>();
    }

    private bool Grounded()
    {
        if(!_isActivePlayer) return false;
        RaycastHit hit;
        float range = _collider.bounds.extents.y + 0.1f;
        Physics.SphereCast(transform.position, 0.3f, -transform.up, out hit, range);
        return hit.collider != null && hit.collider != _collider;
    }
    private void FixedUpdate()
    {
        if(!_isActivePlayer) return;
        Vector3 moveVector = transform.forward * _moveValue.y + transform.right * _moveValue.x; // new Vector3(_moveValue.x, 0, _moveValue.y);
        transform.position += moveVector * _movementSpeed;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if(!_isActivePlayer) return;
        if (context.performed)
        {
            _weapon.Shoot();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!_isActivePlayer) return;
        if(context.performed && Grounded())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(0, _jumpHeight, 0, ForceMode.Impulse);
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if(!_isActivePlayer) return;
        if(context.performed)
        {
            _mousePos = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            _mousePos = Vector2.zero;
        }
        transform.Rotate(Vector3.up * _mousePos.x * 0.2f);
    }

    private void OnDrawGizmos()
    {
        if (_collider == null) return;
        Gizmos.DrawSphere(transform.position - new Vector3(0, _collider.bounds.extents.y, 0), 0.3f);
    }
}
