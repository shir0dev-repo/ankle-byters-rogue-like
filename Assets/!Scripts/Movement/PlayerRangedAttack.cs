using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerRangedAttack : MonoBehaviour
{
    [SerializeField] GameObject _projectilePrefab;


    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Instantiate(_projectilePrefab, transform.position, transform.rotation);
        }
    }
}
