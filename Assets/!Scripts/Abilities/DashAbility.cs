using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{
    private const string _DASH_ACTION_NAME = "DashAbility";
    private InputAction _dashAction;
    private PlayerInputHandler _playerInputHandler;
    private PlayerMovement _playerMovement;

    [SerializeField] private VectorEasingMode _dashCurve;
    [SerializeField] private float _dashDistance = 5.0f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 0.75f;

    private float _cooldownRemaining = 0f;
    private Camera _mainCam;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _dashAction = _playerInputHandler.PlayerActions.FindAction(_DASH_ACTION_NAME);
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

        _playerMovement.ApplyForce(transform.DirectionToMouseWorldSpace(normalized: true) * _dashDistance, _dashDuration, _dashCurve);
        _cooldownRemaining = _dashCooldown;
    }
}
