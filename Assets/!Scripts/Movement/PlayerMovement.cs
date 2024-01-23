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
    [Space, SerializeField] private bool _useDebugging;

    private Vector2 _inputDirection = Vector2.zero;
    private Vector3 _positionLastFrame = Vector3.zero;

    public Vector3 MoveDirection
    {
        get { return (transform.position - _positionLastFrame).normalized; }
    }

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }

    private void FixedUpdate()
    {
        HandleRotation();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (!_moveAction.IsPressed()) return;

        _inputDirection = _moveAction.ReadValue<Vector2>().normalized;
        _positionLastFrame = transform.position;
        transform.position += _moveSpeed * Time.fixedDeltaTime * (Vector3)_inputDirection;
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

    public void ApplyForce(Vector3 forceDirection, float duration, VectorEasingMode easingMode)
        => StartCoroutine(ApplyForceCoroutine(forceDirection, duration, easingMode));


    private IEnumerator ApplyForceCoroutine(Vector3 forceDirection, float duration, VectorEasingMode easingMode)
    {
        if (forceDirection.sqrMagnitude < 0.1f) yield return null;

        _moveAction.Disable();

        Vector3 startPosition = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            float total = timeElapsed / duration;
            transform.position = easingMode.Evaluate(startPosition, forceDirection, total);

            // finish the loop early if player is already at position; in cases where duration is longer than asymptotic value of curve.
            if (Vector3.Distance(transform.position, startPosition + forceDirection) < 0.1f)
                break;

            yield return new WaitForEndOfFrame();
        }

        _moveAction.Enable();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying) return;
        if (!_useDebugging) return;

        Vector3 mousePos = Mouse.current.position.value;
        mousePos.z = -_mainCam.transform.position.z;
        Debug.DrawLine(transform.position, _mainCam.ScreenToWorldPoint(mousePos), Color.red);
    }
}
#endif