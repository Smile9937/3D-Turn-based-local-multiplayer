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
    [SerializeField] private Animator _animator;
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
    private void Start()
    {
        PickRandomIdle();
    }

    private void PickRandomIdle()
    {
        _animator.SetFloat("RandomIdle", UnityEngine.Random.Range(0f, 1f));
    }

    public void ChangeActiveState(bool isActive)
    {
        _isActivePlayer = isActive;
        if(!isActive)
        {
            _moveValue = Vector2.zero;
        }
    }
    public void Move(InputAction.CallbackContext context)
    {
        if(!_isActivePlayer) return;
        _moveValue = context.ReadValue<Vector2>();
    }

    private bool Grounded()
    {
        RaycastHit hit;
        float range = _collider.bounds.extents.y + 0.1f;
        Physics.SphereCast(transform.position, 0.3f, -transform.up, out hit, range);
        return hit.collider != null && hit.collider != _collider;
    }
    private void Update()
    {
        _animator.SetBool("Grounded", Grounded());
    }
    private void FixedUpdate()
    {
        _animator.SetFloat("MoveX", _moveValue.x, 0.1f, Time.deltaTime);
        _animator.SetFloat("MoveZ", _moveValue.y, 0.1f, Time.deltaTime);
        _animator.SetFloat("YVelocity", _rigidbody.velocity.y, 0.1f, Time.deltaTime);

        if(!_isActivePlayer) return;
        Vector3 moveVector = transform.forward * _moveValue.y + transform.right * _moveValue.x;
        transform.position += moveVector * _movementSpeed;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if(!_isActivePlayer) return;
        if (context.performed)
        {
            /*if(!Grounded())
            {
                _animationPlayer.PlayAnimation(_jumpAirAttackAnim);
            }
            else
            {
                _animationPlayer.PlayAnimation(_attackAnim);
            }*/
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
