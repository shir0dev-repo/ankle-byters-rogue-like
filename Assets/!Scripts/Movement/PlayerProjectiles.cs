using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectiles : MonoBehaviour
{
    [SerializeField] private float _bulletSpeed = 20f;

    private void Update()
    {
        ProjectileMovement();
    }

    private void ProjectileMovement()
    {
        // Move the projectile relative to the camera view
        transform.Translate(Vector3.up * _bulletSpeed * Time.deltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}