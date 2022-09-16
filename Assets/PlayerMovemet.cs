using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovemet : MonoBehaviour
{
    [SerializeField] private Weapon weapon;
    [SerializeField] private float _movementSpeed;
    private CharacterController _characterController;
    private Vector2 _moveValue;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    public void Move(InputAction.CallbackContext context)
    {
        _moveValue = context.ReadValue<Vector2>();
    }
    private void FixedUpdate()
    {
        Vector3 moveVector = new Vector3(_moveValue.x, 0, _moveValue.y);
        _characterController.Move(moveVector * _movementSpeed);
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weapon.Shoot();
        }
    }
}
