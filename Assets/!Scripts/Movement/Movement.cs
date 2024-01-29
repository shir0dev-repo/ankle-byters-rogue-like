using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour
{
    [SerializeField] protected float _moveSpeed = 8f;
    [SerializeField]
    protected Vector3 _targetPosition = Vector3.zero;
    protected bool _canMove = true;

    public virtual Vector3 MoveDirection
    {
        get
        {
            return (_targetPosition - transform.position).normalized;
        }
    }

    public virtual bool CanMove
    {
        get { return _canMove; }
        protected set
        {
            _canMove = value;
        }
    }

    protected virtual void FixedUpdate()
    {
        HandleMovement();
        HandleRotation();
    }

    protected abstract void HandleMovement();
    protected abstract void HandleRotation();

    public void ApplyForce(Vector3 force, float duration, VectorEasingMode easingMode)
        => StartCoroutine(ApplyForceCoroutine(force, duration, easingMode));

    protected abstract IEnumerator ApplyForceCoroutine(Vector3 force, float duration, VectorEasingMode mode);
}
