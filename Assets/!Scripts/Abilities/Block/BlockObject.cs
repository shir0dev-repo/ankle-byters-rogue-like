using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class BlockObject : MonoBehaviour
{
    [SerializeField] private int _points = 8;
    [SerializeField] private float _angle = 90.0f;
    [SerializeField] private float _radius = 0.5f;

    private PolygonCollider2D _collider;
    Vector3[] _colliderPoints;
    private void Awake()
    {
        _collider = GetComponent<PolygonCollider2D>();
        _colliderPoints = new Vector3[_points];

        CreateMesh();
    }

    Vector3[] CreateMesh()
    {
        float halfAngle = _angle / 2f;
        float currentAngle = -halfAngle;
        float angleIncrement = _angle / (_points - 1);

        Vector3 centerDirection = transform.DirectionToMouseWorldSpace(true) * _radius;
        Vector3 currentDirection = Quaternion.Euler(0, 0, -halfAngle) * centerDirection;

        Debug.DrawLine(transform.position, currentDirection, Color.yellow, 3f);

        for (int i = 0; i < _points; i++)
        {
            
            Debug.Log(currentAngle.ToString("F2"));
            currentAngle += angleIncrement;
        }

        return null;
    }
}
