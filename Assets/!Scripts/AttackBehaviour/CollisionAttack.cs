using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CollisionAttack : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayermask;
    [SerializeField, Min(1)] private int _damage = 1;
    [SerializeField] private bool _destroyOnImpact = false;

    private void DoDamage(Collider2D other)
    {
        // game object is not target layermask
        if (!_targetLayermask.IsLayer(other.gameObject.layer)) return;

        // target does not have HP component
        if (!other.gameObject.TryGetComponent(out Health healthComp)) return;

        healthComp.TakeDamage(_damage);

        if (_destroyOnImpact)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DoDamage(other);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        DoDamage(other);
    }
}
