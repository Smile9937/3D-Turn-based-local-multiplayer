using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(CapsuleCollider))]
public class UnitMovemet : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Animator _animator;
    [SerializeField] private Weapon _startingWeapon;

    [Header("Stats")]
    [SerializeField] private float _movementSpeed = 0.3f;
    [SerializeField] private float _jumpHeight = 6f;

    [Header("Ground Check")]
    [SerializeField] private Vector3 _groundCheckSize;
    [SerializeField] private float _groundCheckOffset;

    private Rigidbody _rigidbody;
    private Vector2 _moveValue;
    private Collider _collider;
    private Vector2 _mousePos;
    private bool _isActivePlayer;
    private Weapon _currentWeapon;
    private bool _canMove = true;
    private RaycastHit grounCheckRayHit;
    private bool grounCheckRayHitDetect;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    private void Start()
    {
        _currentWeapon = _startingWeapon;
        SwitchWeapon(_startingWeapon);
        PickRandomIdle();
    }
    public void SwitchWeapon(Weapon weapon)
    {
        if (_currentWeapon == null || weapon == null) return;
        _currentWeapon.gameObject.SetActive(false);
        _currentWeapon = weapon;
        _currentWeapon.gameObject.SetActive(true);
    }
    private void PickRandomIdle()
    {
        _animator.SetFloat("RandomIdle", UnityEngine.Random.Range(0f, 1f));
    }

    public void ChangeActiveState(bool isActive)
    {
        _isActivePlayer = isActive;
        ResetMovement();
    }

    private void ResetMovement()
    {
        if (!CanMove())
        {
            _moveValue = Vector2.zero;
        }
    }

    public void ToggleMovement(bool canMove)
    {
        _canMove = canMove;
        ResetMovement();
    }
    public void Move(InputAction.CallbackContext context)
    {
        if (!CanMove()) return;
        _moveValue = context.ReadValue<Vector2>();
    }

    private bool Grounded()
    {
        float range = _collider.bounds.extents.y + _groundCheckOffset;
        grounCheckRayHitDetect = Physics.BoxCast(transform.position, _groundCheckSize, -transform.up, out grounCheckRayHit, transform.rotation, range);
        return grounCheckRayHit.collider != null && grounCheckRayHit.collider != _collider && !grounCheckRayHit.collider.isTrigger;
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

        if (!CanMove()) return;
        Vector3 moveVector = transform.forward * _moveValue.y + transform.right * _moveValue.x;
        transform.position += moveVector * _movementSpeed;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (!CanMove()) return;
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
            _currentWeapon.Shoot();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!CanMove()) return;
        if (context.performed && Grounded())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(0, _jumpHeight, 0, ForceMode.Impulse);
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if (!CanMove()) return;
        if (context.performed)
        {
            _mousePos = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _mousePos = Vector2.zero;
        }
        transform.Rotate(_mousePos.x * 0.2f * Vector3.up);
    }

    private bool CanMove()
    {
        return _canMove && _isActivePlayer;
    }
    void OnDrawGizmos()
    {
        if (_collider == null) return;
        Gizmos.color = Color.red;

        if (grounCheckRayHitDetect)
        {
            Gizmos.DrawRay(transform.position, - transform.up * grounCheckRayHit.distance);
            Gizmos.DrawWireCube(transform.position + -transform.up * grounCheckRayHit.distance, _groundCheckSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position, -transform.up * (_collider.bounds.extents.y + _groundCheckOffset));
            Gizmos.DrawWireCube(transform.position + -transform.up * (_collider.bounds.extents.y + _groundCheckOffset), _groundCheckSize);
        }
    }
}
