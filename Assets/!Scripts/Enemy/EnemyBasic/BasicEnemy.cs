using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float _speed;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, GameManager.Instance.PlayerPosition, _speed * Time.deltaTime);
    }
}
