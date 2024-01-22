using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _fireCooldown = 0.5f; // Adjust the cooldown time as needed

    private float _nextFireTime;
    private const string _RANGED_ATTACK_ACTION_NAME = "RangedAttack";
    private InputAction _rangedAttack;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rangedAttack = _playerInputHandler.PlayerActions.FindAction(_RANGED_ATTACK_ACTION_NAME);
    }

    private void OnEnable()
    {
        _rangedAttack.started += FireProjectile;
    }

    private void OnDisable()
    {
        _rangedAttack.started -= FireProjectile;
    }

    private void FireProjectile(InputAction.CallbackContext obj)
    {
        // attack not ready
        if (_nextFireTime > 0) return;

        Instantiate(_projectilePrefab, transform.position, transform.rotation);
        _nextFireTime = _fireCooldown;
    }

    private void FixedUpdate()
    {
        // decrement timer if necessary
        if (_nextFireTime > 0)
            _nextFireTime -= Time.fixedDeltaTime;
    }
}
