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

    [SerializeField] private float _maxDistance = 7.0f;
    [SerializeField, Min(1)] private float _dashForce = 1.0f;
    [SerializeField] private float _dashDuration = 0.5f;

    private Camera _mainCam;
    private float _dashCooldown = 2.5f;
    private float _cooldownRemaining = 0.0f;
    private bool _isDashing = false;


    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _mainCam = Camera.main;

        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _dashAction = _playerInputHandler.PlayerActions.FindAction(_DASH_ACTION_NAME);

        _cooldownRemaining = _dashCooldown;
    }

    private void OnEnable()
    {
        _dashAction.started += ExecuteDash;
    }

    private void Update()
    {
        if (_cooldownRemaining > 0.0f)
            _cooldownRemaining -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _dashAction.started -= ExecuteDash;
    }

    private void ExecuteDash(InputAction.CallbackContext obj)
    {
        // dash not ready
        if (_isDashing) return; // || _cooldownRemaining > 0.0f) return;

        Vector3 dashDirection = _mainCam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        dashDirection.z = 0;
        dashDirection.Normalize();
        
        Debug.DrawRay(transform.position, dashDirection * _maxDistance, Color.green, 2f);
        transform.Translate(dashDirection * _maxDistance, Space.World);
        //StartCoroutine(DashCoroutine(dashDirection));
    }

    private IEnumerator DashCoroutine(Vector3 dashDirection)
    {
        _isDashing = true;
        _playerMovement.CanMove = false;
        float timeElapsed = 0.0f;

        while (timeElapsed < _dashDuration)
        {
            timeElapsed += Time.deltaTime;

            float total = timeElapsed / _dashDuration;
            transform.position = Vector3.Lerp(transform.position, dashDirection, total);

            yield return new WaitForEndOfFrame();
        }

        yield return null;

        _playerMovement.CanMove = true;
        _isDashing = false;
    }
}
