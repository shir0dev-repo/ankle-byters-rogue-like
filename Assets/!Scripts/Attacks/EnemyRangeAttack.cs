using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _minFireCooldown = 1f;
    [SerializeField] float _maxFireCooldown = 3f;

    private float _cooldownRemaining;
    private float _currentCooldown;

    private void Start()
    {
        _cooldownRemaining = Random.Range(_minFireCooldown, _maxFireCooldown);
        _currentCooldown = _cooldownRemaining;
    }

    private void Update()
    {
        _currentCooldown -= Time.deltaTime;

        if (_currentCooldown <= 0)
        {
            FireProjectile();

            _cooldownRemaining = Random.Range(_minFireCooldown, _maxFireCooldown);
            _currentCooldown = _cooldownRemaining;
        }
    }

    private void FireProjectile()
    {
        if (PlayerManager.Instance == null) return;

        Projectile p = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        p.Init(PlayerManager.Instance.GetPlayerPosition());
    }

}