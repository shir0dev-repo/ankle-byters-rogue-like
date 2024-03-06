using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMovement : Movement
{
    private const string _MOVE_ACTION_NAME = "Move";

    [SerializeField] private bool _useDebugging = false;

    private InputAction _moveAction;
    private PlayerInputHandler _playerInputHandler;
    private Rigidbody2D _rigidbody;
    private Vector2 _inputDirection = Vector2.zero;



    public override Vector3 MoveDirection
    {
        get
        {
            return _inputDirection.normalized;
        }
    }

    public override bool CanMove
    {
        get { return _canMove; }
        protected set
        {
            if (value)
                _moveAction.Enable();
            else
                _moveAction.Disable();
            _canMove = value;
        }
    }

    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }

    protected override void HandleMovement()
    {
        _inputDirection = _moveAction.ReadValue<Vector2>().normalized;

        if (_inputDirection == Vector2.zero) return;

        _inputDirection = _moveAction.ReadValue<Vector2>().normalized;
        _rigidbody.MovePosition(transform.position + _moveSpeed * Time.fixedDeltaTime * (Vector3)_inputDirection);
        //transform.position += _moveSpeed * Time.fixedDeltaTime * (Vector3)_inputDirection;
    }

    protected override void HandleRotation()
    {
        Vector3 mousePos = Mouse.current.position.value;
        mousePos.z = -_mainCam.transform.position.z;
        Vector3 transformScreenPosition = _mainCam.WorldToScreenPoint(transform.position);

        mousePos.x -= transformScreenPosition.x;
        mousePos.y -= transformScreenPosition.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle - 90.0f);
    }

    protected override IEnumerator ApplyForceCoroutine(Vector3 forceDirection, float duration, VectorEasingMode easingMode)
    {
        if (forceDirection.sqrMagnitude < 0.1f) yield return null;

        CanMove = false;

        Vector3 startPosition = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed <= duration)
        {
            timeElapsed += Time.deltaTime;
            float total = timeElapsed / duration;
            _rigidbody.MovePosition(easingMode.Evaluate(startPosition, forceDirection, total));
            //transform.position = easingMode.Evaluate(startPosition, forceDirection, total);

            // finish the loop early if player is already at position; in cases where duration is longer than asymptotic value of curve.
            if (Vector3.Distance(transform.position, startPosition + forceDirection) < 0.1f)
                break;

            yield return new WaitForEndOfFrame();
        }

        CanMove = true;
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