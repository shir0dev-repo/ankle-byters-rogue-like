using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;
    [SerializeField] float _minFireCooldown = 1f;
    [SerializeField] float _maxFireCooldown = 3f;

    private float _nextFireTime;

    private void Start()
    {
        _nextFireTime = Time.time + Random.Range(_minFireCooldown, _maxFireCooldown);
    }

    private void FixedUpdate()
    {
        if (Time.time >= _nextFireTime)
        {
            FireProjectile();

            _nextFireTime = Time.time + Random.Range(_minFireCooldown, _maxFireCooldown);
        }
    }

    private void FireProjectile()
    {
        Instantiate(_projectilePrefab, transform.position, Quaternion.identity);
    }

}   