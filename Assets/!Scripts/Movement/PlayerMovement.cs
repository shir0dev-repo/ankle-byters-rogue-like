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
    private Vector3 _direction = Vector3.zero;

    private Camera _mainCam;
    [Space, SerializeField] private bool _useDebugging;

    private void Awake()
    {
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }

    private void FixedUpdate()
    {
        HandleRotation();
        
        // move action is in progress
        if (_moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1f)
            HandleMovement();


    }

    private void HandleMovement()
    {
        _direction = _moveAction.ReadValue<Vector2>().normalized;
        transform.position += _moveSpeed * Time.fixedDeltaTime * _direction;
    }

    private void HandleRotation()
    {
        //float angle = Mathf.Atan2(_direction.x, _direction.y) * Mathf.Rad2Deg * -1f;
        //Quaternion targetRotation = Quaternion.Euler(angle * Vector3.forward);
        //
        //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);

        Vector3 lookDir = transform.position - _mainCam.ScreenToWorldPoint(Mouse.current.position.value);
        
        if (lookDir != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(lookDir, Vector3.forward);
            transform.rotation = Quaternion.Euler(0, 0, lookRotation.eulerAngles.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        if (!_useDebugging) return;

        Debug.DrawLine(transform.position, _mainCam.ScreenToWorldPoint(Mouse.current.position.value), Color.red);
        
    }
}
