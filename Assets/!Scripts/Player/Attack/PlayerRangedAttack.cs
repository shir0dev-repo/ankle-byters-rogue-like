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
    private const string _RANGED_ATTACKS_ACTION_NAME = "RangedAttack";
    private InputAction _rangedAttack;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _rangedAttack = _playerInputHandler.PlayerActions.FindAction(_RANGED_ATTACKS_ACTION_NAME);
    }

    private void Update()
    {
        if (_rangedAttack.ReadValue<float>() > 0.5f && Time.time >= _nextFireTime)
        {
            Projectile p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
            Vector3 mousePos = Mouse.current.position.value;
            mousePos.z = -Camera.main.transform.position.z;

            p.Init(Camera.main.ScreenToWorldPoint(mousePos));
            _nextFireTime = Time.time + _fireCooldown;
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
