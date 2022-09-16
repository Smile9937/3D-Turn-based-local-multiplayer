using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerMovemet : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float jumpHeight;
    private Rigidbody _rigidbody;
    private Vector2 _moveValue;
    [SerializeField]private Collider _collider;
    Vector2 mousePos;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }

    private bool Grounded()
    {
        RaycastHit hit;
        float range = _collider.bounds.extents.y + 0.1f;
        Physics.SphereCast(transform.position, 0.3f, -transform.up, out hit, range);
        return hit.collider != null && hit.collider != _collider;
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = transform.forward * _moveValue.y + transform.right * _moveValue.x; // new Vector3(_moveValue.x, 0, _moveValue.y);
        transform.position += moveVector * _movementSpeed;
    }
    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _weapon.Shoot();
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if(context.performed && Grounded())
        {
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(0, jumpHeight, 0, ForceMode.Impulse);
        }
    }
    public void Look(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            mousePos = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            mousePos = Vector2.zero;
        }
        transform.Rotate(Vector3.up * mousePos.x * 0.2f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position - new Vector3(0, _collider.bounds.extents.y + 0.1f, 0), 0.3f);
    }
}
