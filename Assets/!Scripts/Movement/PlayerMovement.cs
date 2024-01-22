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
        Vector3 mousePos = Mouse.current.position.value;
        mousePos.z = -_mainCam.transform.position.z; 
        Vector3 transformScreenPosition = _mainCam.WorldToScreenPoint(transform.position);

        mousePos.x -= transformScreenPosition.x;
        mousePos.y -= transformScreenPosition.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }

    public void ApplyForce(Vector3 forceDirection, bool interruptMovement, float duration, VectorEasingMode easingMode, Action callback = null)
    {
        if (forceDirection.sqrMagnitude < 0.1f) return;

        if (interruptMovement)
            CanMove = false;

        StartCoroutine(ApplyForceCoroutine(forceDirection, duration, interruptMovement, easingMode, callback));
    }


    private IEnumerator ApplyForceCoroutine(Vector3 forceDirection, float duration, bool interruptMovement, VectorEasingMode easingMode, System.Action callback = null)
    {
        Vector3 startPosition = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            float total = timeElapsed / duration;
            transform.position = easingMode.Get(startPosition, forceDirection, total);

            Debug.Log(Vector3.Distance(transform.position, forceDirection));
            if (Vector3.Distance(transform.position, forceDirection) < 0.5f)
                break;
            yield return new WaitForEndOfFrame();
        }
        callback?.Invoke();

        yield return null;
        if (interruptMovement && !CanMove)
            CanMove = true;

        
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        if (!_useDebugging) return;

        Vector3 mousePos = Mouse.current.position.value;
        mousePos.z = -_mainCam.transform.position.z;
        Debug.DrawLine(transform.position, _mainCam.ScreenToWorldPoint(mousePos), Color.red);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hello");
    }
}
