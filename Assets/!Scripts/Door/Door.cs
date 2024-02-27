using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    //instead of storing the player, use a layermask for it
    [SerializeField] private LayerMask _playerLayermask;
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    public bool IsLocked { get; set; }
    public bool playerPassedThrough = false;

    public EnemySpawner enemySpawner;
    private bool enemiesSpawned = false;
    private bool bossSpawned = false;

    private void Update()
    {
        if (door1 != null && playerPassedThrough && !enemiesSpawned)
        {
            LockDoor();
            enemySpawner.SpawnEnemies();
            enemiesSpawned = true;
        }
    }
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

        playerPassedThrough = true;
    }
    public void LockDoor()
    {
        IsLocked = true;
    }
}
