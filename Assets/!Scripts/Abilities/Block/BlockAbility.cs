using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class BlockAbility : MonoBehaviour
{
    private const string _BLOCK_ABILITY_NAME = "BlockAbility";
    private PlayerInputHandler _playerInputHandler;
    private InputAction _blockAction;

    [SerializeField] private GameObject _blockObjectPrefab;
    [Space]
    [SerializeField] private float _blockAngleDegrees = 90.0f;
    [SerializeField] private float _blockDistance = 0.5f;
    [SerializeField] private float _blockLifetime = 1.0f;
    [SerializeField] private float _blockCooldown = 1.0f;

    private BlockObject _blockObject;
    private float _cooldownRemaining = 0.0f;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _blockAction = _playerInputHandler.PlayerActions.FindAction(_BLOCK_ABILITY_NAME);

        _blockObject = Instantiate(_blockObjectPrefab, transform.position, Quaternion.identity).GetComponent<BlockObject>();
        _blockObject.transform.SetParent(transform, false);

        _blockObject.Init(_blockAngleDegrees, _blockDistance, _blockLifetime);
    }

    private void OnEnable()
    {
        _blockAction.started += ExecuteBlock;
    }

    private void Update()
    {
        if (_cooldownRemaining > 0.0f)
            _cooldownRemaining -= Time.deltaTime;
    }

    private void OnDisable()
    {
        _blockAction.started -= ExecuteBlock;
    }

    private void ExecuteBlock(InputAction.CallbackContext obj)
    {
        // block not ready
        if (_cooldownRemaining > 0.0f) return;

        _blockObject.gameObject.SetActive(true);
        _cooldownRemaining = _blockCooldown;
    }
}
