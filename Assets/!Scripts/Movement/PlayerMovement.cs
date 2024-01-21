using System;
using System.Collections;
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

    public bool CanMove { get; set; }

    private void Awake()
    {
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
        CanMove = true;
    }

    private void FixedUpdate()
    {
        HandleRotation();
        
        // able to move && move action is in progress
        if (CanMove && _moveAction.ReadValue<Vector2>().sqrMagnitude > 0.1f)
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

    public void ApplyForce(Vector3 forceDirection, bool interruptMovement, float duration, Func<Vector3, float, Vector3> positionDelegate)
    {
        if (forceDirection.sqrMagnitude < 0.1f) return;

        if (interruptMovement)
            CanMove = false;

        StartCoroutine(ApplyForceCoroutine(forceDirection, duration, interruptMovement, positionDelegate));
    }

    private IEnumerator ApplyForceCoroutine(Vector3 forceDirection, float duration, bool interruptMovement, Func<Vector3, float, Vector3> positionDelegate)
    {
        Vector3 startPosition = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            float total = timeElapsed / duration;
            transform.position = startPosition + positionDelegate(forceDirection, total);

            yield return new WaitForEndOfFrame();
        }

        yield return null;
        if (interruptMovement && !CanMove)
            CanMove = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        if (!_useDebugging) return;

        Debug.DrawLine(transform.position, _mainCam.ScreenToWorldPoint(Mouse.current.position.value), Color.red);
    }
}
