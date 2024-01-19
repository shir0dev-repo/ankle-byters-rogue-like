using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float fireCooldown = 0.5f; // Adjust the cooldown time as needed

    private float nextFireTime;
    private const string _RANGEDATTACKS_ACTION_NAME = "RangedAttack";
    private InputAction _rangedAttack;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rangedAttack = _playerInputHandler.PlayerActions.FindAction(_RANGEDATTACKS_ACTION_NAME);
    }

    private void FixedUpdate()
    {
        if (_rangedAttack.ReadValue<float>() > 0.5f && Time.time >= nextFireTime)
        {
            Instantiate(_projectilePrefab, transform.position, transform.rotation);
            nextFireTime = Time.time + fireCooldown;
        }
    }
    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(1))
    //    {
    //        Instantiate(_projectilePrefab, transform.position, transform.rotation);
    //    }
    //}
}
