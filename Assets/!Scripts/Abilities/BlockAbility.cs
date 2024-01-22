using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlockAbility : MonoBehaviour
{
    private const string _BLOCK_ABILITY_NAME = "BlockAbility";
    private PlayerInputHandler _playerInputHandler;
    private InputAction _blockAction;

    [SerializeField] private float _blockAngleDegrees = 90.0f;
    [SerializeField] private float _blockCooldown = 1.0f;

}
