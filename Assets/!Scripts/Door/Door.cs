using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayer;

    public bool IsLocked { get; set; }
    public void Lock()
    {
        IsLocked = true;
    }
    public void Unlock()
    {
        IsLocked = false;
    }
}
