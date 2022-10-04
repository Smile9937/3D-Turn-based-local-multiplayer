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
    [SerializeField] private float _startMovementSpeed = 10f;
    [SerializeField] private float _jumpHeight = 6f;
    [SerializeField] private float _startMoveDistance;

    [Header("Ground Check")]
    [SerializeField] private Vector3 _groundCheckSize;
    [SerializeField] private float _groundCheckOffset;

    [Header("Settings")]
    [SerializeField] private float _rotateSensitivity = 0.5f;

    [Header("Debug Values")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _moveDistance;

    private Rigidbody _rigidbody;
    private Vector2 _moveValue;
    private Collider _collider;
    private Vector2 _mousePos;
    private bool _isActiveUnit;
    private Weapon _currentWeapon;
    private bool _canMove = true;
    private RaycastHit grounCheckRayHit;
    private bool grounCheckRayHitDetect;
    private Vector3 _moveDir;
    private bool _moving;
    private Unit _unit;

    //Animations
    private string _randomIdle = "RandomIdle";
    private string _grounded = "Grounded";
    private string _moveX = "MoveX";
    private string _moveZ = "MoveZ";
    private string _yVelocity = "YVelocity";
    private string _attack = "Attack";
    private string _jump = "Jump";

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _unit = GetComponent<Unit>();
    }
    private void Start()
    {
        _currentWeapon = _startingWeapon;
        _movementSpeed = _startMovementSpeed;
        _moveDistance = _startMoveDistance;
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
        _animator.SetFloat(_randomIdle, UnityEngine.Random.Range(0f, 1f));
    }

    public void ChangeActiveState(bool isActive)
    {
        _isActiveUnit = isActive;
        if(_isActiveUnit)
        {
            _moveDistance = _startMoveDistance;
        }
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

        if(context.phase == InputActionPhase.Started)
        {
            _moving = true;
            StartCoroutine(ReduceMove());
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            _moving = false;
            StopCoroutine("ReduceMove");
        }
    }
    private IEnumerator ReduceMove()
    {
        while(_moving)
        {
            _moveDistance--;
            if(_moveDistance <= 0)
            {
                _canMove = false;
                _moving = false;
                _moveValue = Vector2.zero;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private bool Grounded()
    {
        float range = _collider.bounds.extents.y + _groundCheckOffset;
        grounCheckRayHitDetect = Physics.BoxCast(transform.position, _groundCheckSize, -transform.up, out grounCheckRayHit, transform.rotation, range);
        return grounCheckRayHit.collider != null && grounCheckRayHit.collider != _collider && !grounCheckRayHit.collider.isTrigger;
    }
    private void Update()
    {
        _animator.SetBool(_grounded, Grounded());
    }
    private void FixedUpdate()
    {
        _animator.SetFloat(_moveX, _moveValue.x, 0.1f, Time.deltaTime);
        _animator.SetFloat(_moveZ, _moveValue.y, 0.1f, Time.deltaTime);
        _animator.SetFloat(_yVelocity, _rigidbody.velocity.y, 0.1f, Time.deltaTime);

        if (!CanMove()) return;

        Vector3 moveVector = transform.forward * _moveValue.y + transform.right * _moveValue.x;
        _moveDir = transform.position + (moveVector * _movementSpeed * Time.deltaTime);

        _rigidbody.MovePosition(_moveDir);
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (!_isActiveUnit) return;
        if (context.performed)
        {
            _animator.SetTrigger(_attack);
            _currentWeapon.Shoot();
        }
        if(context.canceled)
        {
            _unit.DoneWithTurn();
        }
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (!CanMove()) return;
        if (context.performed && Grounded())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(0, _jumpHeight, 0, ForceMode.Impulse);
            _animator.SetTrigger(_jump);
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if (!_isActiveUnit) return;
        if (context.performed)
        {
            _mousePos = context.ReadValue<Vector2>();
        }
        else if (context.canceled)
        {
            _mousePos = Vector2.zero;
        }
        transform.Rotate(_mousePos.x * _rotateSensitivity * Vector3.up);
    }

    private bool CanMove()
    {
        return _canMove && _isActiveUnit;
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
