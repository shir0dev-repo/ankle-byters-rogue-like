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

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }

    private void Update()
    {
        // move keys are not pressed
        if (_moveAction.ReadValue<Vector2>().sqrMagnitude < 0.1f)
            return;

        _direction = _moveAction.ReadValue<Vector2>().normalized;
        transform.position += _moveSpeed * Time.deltaTime * _direction;

    }
}
