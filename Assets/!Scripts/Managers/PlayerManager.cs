using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Transform _player;
    public LayerMask PlayerLayer { get; private set; }

    public static Action OnPlayerDeath;
    public Health PlayerHealth { get; private set; }

    private void OnEnable()
    {
        FloorManager.OnRoomEntered += SetPlayerPosition;
    }

    protected override void Awake()
    {
        base.Awake();

        PlayerLayer = LayerMask.NameToLayer("Player");
    }

    private void OnDisable()
    {
        FloorManager.OnRoomEntered -= SetPlayerPosition;
    }

    public GameObject SpawnPlayer(Vector3 position)
    {
        // would not work for multiplayer
        if (_player != null) return _player.gameObject;

        _player = Instantiate(_playerPrefab).transform;
        _player.position = position;

        PlayerHealth = _player.GetComponent<Health>();
        PlayerHealth.OnDeath += BroadcastPlayerDeath;

        return _player.gameObject;
    }

    private void BroadcastPlayerDeath()
    {
        OnPlayerDeath?.Invoke();
    }
    public Vector3 GetPlayerPosition()
    {
        if (_player == null) return Vector3.zero;
        return _player.position;
    }

    private void SetPlayerPosition(Room room, Door door)
    {
        // entered room
        Vector3 enteredRoomPos = room.transform.position;
        Vector3 playerPos = enteredRoomPos;

        Vector2 playerBounds = _player.GetComponent<Collider2D>().bounds.extents * 2f;
        Vector3 doorDirection = (room.transform.position - door.transform.position).normalized;
        float scaleX = (Room.ROOM_SCALE_X / 2f - playerBounds.x) * 0.8f;
        float scaleY = (Room.ROOM_SCALE_Y / 2f - playerBounds.y) * 0.8f;

        playerPos.x -= scaleX * doorDirection.x;
        playerPos.y -= scaleY * doorDirection.y;


        _player.position = playerPos;
    }
}
