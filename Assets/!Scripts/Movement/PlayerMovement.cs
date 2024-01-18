using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";
    private InputAction _moveAction;
    private PlayerInputHandler _playerInputHandler;

    [SerializeField] private float _moveSpeed = 8.0f;
    [SerializeField] private float _rotationSpeed = 15.0f;
    private Vector3 _direction = Vector3.zero;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }

    private void FixedUpdate()
    {
        // move action is in progress
        if (_moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1f)
        {
            HandleMovement();
            HandleRotation();
        }
    }

    private void HandleMovement()
    {
        _direction = _moveAction.ReadValue<Vector2>().normalized;
        transform.position += _moveSpeed * Time.fixedDeltaTime * _direction;
    }

    private void HandleRotation()
    {
        float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg * -1f;
        Quaternion targetRotation = Quaternion.Euler(angle * Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}
