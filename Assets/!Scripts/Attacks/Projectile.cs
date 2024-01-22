using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 12f;
    [SerializeField] private int _damage = 2;
    [SerializeField] private LayerMask _targetLayermask;

    private void FixedUpdate()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        // Move the projectile relative to the camera view
        transform.Translate(Vector3.up * _bulletSpeed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_targetLayermask.IsLayer(collision.gameObject.layer)) return;
        if (!collision.gameObject.TryGetComponent(out Health healthComp)) return;

        healthComp.TakeDamage(_damage);
    }
}