using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMovement : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";

    [SerializeField] private float _moveSpeed = 8.0f;

    private bool _canMove = false;

    private InputAction _moveAction;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _moveAction = _playerInputHandler.PlayerActions.FindAction(_MOVE_ACTION_NAME);
    }
}
