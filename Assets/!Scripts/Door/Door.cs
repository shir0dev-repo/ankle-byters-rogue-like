using System;
using UnityEditor.Overlays;
using UnityEngine;

public class Door : MonoBehaviour
{
    //instead of storing the player, use a layermask for it
    [SerializeField] private LayerMask _playerLayermask;

    public bool IsLocked { get; set; }

    public Vector3 GetAdjacentRoom()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.z = 0;
        
        Vector3 directionFromCamera = (transform.position - cameraPos).normalized;

        directionFromCamera.x = MathF.Round(directionFromCamera.x * CameraManager.ScreenExtents.x * 2, 2);
        directionFromCamera.y = MathF.Round(directionFromCamera.y * CameraManager.ScreenExtents.y * 2, 2);


        return directionFromCamera;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (IsLocked || !_playerLayermask.IsLayer(collision.gameObject.layer)) return;
        
        GameManager.Instance.CameraManager.EnterRoom(this);
    }
}
