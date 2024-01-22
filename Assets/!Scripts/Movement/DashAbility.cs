using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DashAbility : MonoBehaviour
{
    private const string _DASH_ACTION_NAME = "Dash";
    private InputAction _dashAction;
    private PlayerInputHandler _playerInputHandler;

    [SerializeField] private PlayerMovement _playerMovement;
    [Space]
    [SerializeField] private float _dashDistance = 5.0f;
    [SerializeField] private float _dashDuration = 0.5f;

    private Camera _mainCam;
    private bool _isDashing = false;


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

    private void OnDisable()
    {
        _dashAction.started -= ExecuteDash;
    }

    private void ExecuteDash(InputAction.CallbackContext obj)
    {
        // dash not ready
        if (_isDashing) return;

        _isDashing = true;
        _playerMovement.ApplyForce(GetDashDirection(), true, _dashDuration, VectorEasingMode.EaseOutExpo, () => _isDashing = false);
    }

    private Vector3 GetDashDirection()
    {
        // get mouse position
        Vector3 mousePosPX = Mouse.current.position.value;

        // offset mousepos by camera's z-position to stay at z = 0
        mousePosPX.z = -_mainCam.transform.position.z;

        // dash direction with correct magnitude
        return (_mainCam.ScreenToWorldPoint(mousePosPX) - transform.position) * _dashDistance;
    }
}
