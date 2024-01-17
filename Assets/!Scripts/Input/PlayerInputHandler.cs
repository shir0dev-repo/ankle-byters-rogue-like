using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PlayerInputHandler : MonoBehaviour
{
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
        _playerInputActionsAsset = new PlayerInputActionsAsset();
    }

    private void OnEnable()
    {
        PlayerActions.Enable();
    }
    private void OnDisable()
    {
        PlayerActions.Disable();
    }
}
