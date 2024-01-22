using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class BlockObject : MonoBehaviour
{
    [SerializeField] private int _colliderPointCount = 8;
    [SerializeField] private LayerMask _attackLayer;
    private float _angle = 90.0f;
    private float _radius = 0.5f;
    private float _lifetime = 1.0f;
    private float _lifetimeRemaining = 0.0f;

    private PolygonCollider2D _collider;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public bool HasBeenInitialized { get; private set; }

    public void Init(float angle, float radius, float lifetime)
    {
        // object already initialized.
        if (HasBeenInitialized) return;

        _angle = angle;
        _radius = radius;
        _lifetime = lifetime;
        _lifetimeRemaining = lifetime;

        _collider = GetComponent<PolygonCollider2D>();
        _collider.points = CreateMesh();

        HasBeenInitialized = true;
    }

    private void OnEnable()
    {
        _lifetimeRemaining = _lifetime;
        StartCoroutine(LifetimeCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private Vector2[] CreateMesh()
    {
        // add one for the tip of the cone
        Vector2[] points = new Vector2[_colliderPointCount + 1];
        points[^1] = transform.position;

        float halfAngle = _angle / 2f;
        float angleIncrement = -( _angle / (_colliderPointCount - 1));

        Vector3 currentDirection = Quaternion.Euler(0, 0, halfAngle) * transform.parent.up;

        for (int i = 0; i < _colliderPointCount; i++)
        {
            points[i] = currentDirection * _radius;
            Debug.DrawLine(transform.position, currentDirection, Color.yellow, 3f);
            currentDirection = Quaternion.Euler(0, 0, angleIncrement) * currentDirection;
        }

        return points;
    }

    private IEnumerator LifetimeCoroutine()
    {
        while (_lifetimeRemaining >= 0.0f)
        {
            _lifetimeRemaining -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_attackLayer.IsLayer(collision.gameObject.layer)) return;
        if (!collision.gameObject.TryGetComponent<Projectile>(out _)) return;

        Destroy(collision.gameObject);
    }
}
