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

        // get mouse position
        Vector3 mousePosPX = Mouse.current.position.value;

        // offset mousepos by camera's z-position to stay at z = 0
        mousePosPX.z = -_mainCam.transform.position.z;

        // dash direction with correct magnitude
        Vector3 dashDirection = (_mainCam.ScreenToWorldPoint(mousePosPX) - transform.position).normalized * _dashDistance;
        
        StartCoroutine(DashCoroutine(dashDirection));
        Debug.DrawLine(transform.position, dashDirection + transform.position, Color.green, 2f);
    }

    private IEnumerator DashCoroutine(Vector3 dashDirection)
    {
        _isDashing = true;
        _playerMovement.CanMove = false;
        Vector3 startingPosition = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed < _dashDuration)
        {
            timeElapsed += Time.deltaTime;

            float total = timeElapsed / _dashDuration;
            float easeExpo = total == 1 ? 1 : 1 - Mathf.Pow(2.0f, -10.0f * total);

            // get position this frame:
            // - take starting position and add a percentage of end position * multiplied by any interpolation.
            transform.position = startingPosition + Vector3.Lerp(Vector3.zero, dashDirection, easeExpo);

            yield return new WaitForEndOfFrame();
        }

        yield return null;

        _playerMovement.CanMove = true;
        _isDashing = false;
    }
}
