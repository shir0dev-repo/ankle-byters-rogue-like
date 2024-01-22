using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayermask;
    [SerializeField, Min(1)] private int _damage = 1;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // game object is not target layermask
        if (!_targetLayermask.IsLayer(collision.gameObject.layer)) return;

        // target does not have HP component
        if (!collision.gameObject.TryGetComponent(out Health healthComp)) return;

        healthComp.TakeDamage(_damage);
    }
}
