using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Projectile : MonoBehaviour
{
    private const float _TIME_TO_KILL = 5f;

    [SerializeField] private float _speed = 12f;

    private Vector3 _direction = Vector3.zero;
    private float _lifetime = _TIME_TO_KILL;
    public void Init(Transform target, float speed = 12f)
    {
        _speed = speed;
        _direction = (target.position - transform.position).normalized;
    }

    public void Init(Vector3 targetPosition, float speed = 12f)
    {
        _speed = speed;
        _direction = (targetPosition - transform.position).normalized;
    }

    private void Update()
    {
        _lifetime -= Time.deltaTime;
        if (_lifetime < 0)
            Destroy(gameObject);
    }
    private void FixedUpdate() => Move();

    private void Move()
    {
        // Move the projectile relative to the camera view
        transform.Translate(_direction * _speed * Time.fixedDeltaTime);
    }
}