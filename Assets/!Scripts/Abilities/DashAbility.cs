using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{
    private const string _DASH_ACTION_NAME = "DashAbility";
    private InputAction _dashAction;
    private PlayerInputHandler _playerInputHandler;

    [SerializeField] private PlayerMovement _playerMovement;
    [Space]
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

        _playerMovement.ApplyForce(GetDashDirection(), true, _dashDuration, VectorEasingMode.EaseOutExpo);
        _cooldownRemaining = _dashCooldown;
    }

    private Vector3 GetDashDirection()
    {
        // get mouse position
        Vector3 mousePosPX = Mouse.current.position.value;

        // offset mousepos by camera's z-position to stay at z = 0
        mousePosPX.z = -_mainCam.transform.position.z;

        // dash direction with correct magnitude
        return (_mainCam.ScreenToWorldPoint(mousePosPX) - transform.position).normalized * _dashDistance;
    }
}
