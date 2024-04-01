using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerInputHandler : MonoBehaviour
{
    private const string _PAUSE_ACTION_NAME = "Pause";
    private InputAction _pauseAction;

    private PlayerInputActionsAsset _playerInputActionsAsset;
    public PlayerInputActionsAsset PlayerActions
    {
        get
        {
            _playerInputActionsAsset ??= new PlayerInputActionsAsset();
            return _playerInputActionsAsset;
        }
    }

    private void Awake()
    {
        _playerInputActionsAsset ??= new PlayerInputActionsAsset();
        _pauseAction = _playerInputActionsAsset.FindAction(_PAUSE_ACTION_NAME);
    }
    private void OnEnable()
    {
        _pauseAction.started += PauseGame;
        PlayerActions.Enable();

    }
    private void OnDisable()
    {
        PlayerActions.Disable();
        _pauseAction.started -= PauseGame;
    }

    private void PauseGame(InputAction.CallbackContext context)
    {
        GameManager.Instance.PauseGame();
    }
}
