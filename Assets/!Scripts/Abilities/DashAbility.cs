using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(PlayerMovement))]
public class DashAbility : MonoBehaviour
{
    private const string _DASH_ACTION_NAME = "DashAbility";
    private InputAction _dashAction;

    private KeyControl _shiftKey;
    private PlayerInputHandler _playerInputHandler;
    private PlayerMovement _playerMovement;

    [SerializeField] private VectorEasingMode _dashCurve;
    [SerializeField] private float _dashDistance = 5.0f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 0.75f;

    private float _cooldownRemaining = 0f;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _dashAction = _playerInputHandler.PlayerActions.FindAction(_DASH_ACTION_NAME);

        _shiftKey = Keyboard.current.leftShiftKey;
    }

    private void OnEnable()
    {
        _dashAction.started += ExecuteDash;
    }

    private void Update()
    {
        if (_cooldownRemaining > 0f)
            _cooldownRemaining -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _dashAction.started -= ExecuteDash;
    }

    private void ExecuteDash(InputAction.CallbackContext obj)
    {
        if (_cooldownRemaining > 0f) return;

        if (_shiftKey.isPressed || _playerMovement.MoveDirection == Vector3.zero)
            _playerMovement.ApplyForce(transform.DirectionToMouseWorldSpace(true) * _dashDistance, _dashDuration, _dashCurve);
        else
            _playerMovement.ApplyForce(_playerMovement.MoveDirection * _dashDistance, _dashDuration, _dashCurve);
        
        _cooldownRemaining = _dashCooldown;
    }
}
