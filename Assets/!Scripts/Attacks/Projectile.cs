using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 12f;

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
}