using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    private Player _player;
    [SerializeField] private Camera _camera;

    private bool _isAttacking;
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    public void MoveDirection(InputAction.CallbackContext context)
    {
        _player.MoveDirection = context.ReadValue<Vector2>();
    }
    public void TargetPos(InputAction.CallbackContext context)
    {
        //_player.TargetPos = _camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
    }
    public void Interact(InputAction.CallbackContext context)
    {
        _player.Interact();
    }
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
            _isAttacking = true;
        if (context.canceled)
            _isAttacking = false;
    }
    public void Reload(InputAction.CallbackContext context)
    {
        _player.Reload();
    }
    void FixedUpdate()
    {
        _player.TargetPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_isAttacking)
            _player.Attack();
    }
}
