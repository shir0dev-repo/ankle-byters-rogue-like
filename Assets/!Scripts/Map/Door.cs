using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private bool _isLocked = true;

    private Collider2D _collider;

    public bool IsLocked => _isLocked;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void Lock()
    {
        _isLocked = true;
        _collider.isTrigger = false;
    }
    public void Unlock()
    {
        _isLocked = false;
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isLocked) return;
        else if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

        FloorManager.OnDoorCollision?.Invoke(this);
    }
}
