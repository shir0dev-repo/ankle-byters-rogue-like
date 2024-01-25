using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private GameObject _playerPrefab;
    [SerializeField, ReadOnly] private Transform _player;

    public void SpawnPlayer(Vector3 position)
    {
        // would not work for multiplayer
        if (_player != null) return;

        _player = Instantiate(_playerPrefab).transform;
        _player.position = position;
    }

    public Vector3 GetPlayerPosition()
    {
        if (_player == null) return Vector3.zero;
        return _player.position;
    }
}
