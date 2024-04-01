using System;
using System.Collections;
using System.Collections.Generic;
using DungeonMaster2D;
using UnityEngine;

[SelectionBase]
public class Room : MonoBehaviour
{
    public const float ROOM_SCALE_X = 17.98f;
    public const float ROOM_SCALE_Y = 10.0f;

    [SerializeField] private bool _isLocked;
    private Dictionary<Door, Vector3> _doorRoomPositionDictionary = new();

    public bool Visible { get; private set; }
    public bool Cleared { get; private set; }
    public Node Node { get; private set; }
    public Dictionary<Door, Vector3> DoorRoomPositionDictionary { get { return _doorRoomPositionDictionary; } }

    public void Init(Node node, Node startingNode)
    {
        Visible = false;
        Cleared = false;
        Node = node;

        transform.position = GetScaledPosition(startingNode);

        GetDoors();
        LockRoom();
    }

    private void GetDoors()
    {
        BoxCollider2D[] doorColliders = gameObject.GetComponentsInChildren<BoxCollider2D>();
        if (doorColliders.Length >= 4)
            Debug.LogError(this + "!!!!!!!!!!!!!!!!!!!!!");
        foreach (BoxCollider2D doorCollider in doorColliders)
        {
            Door key = doorCollider.gameObject.AddComponent<Door>();
            Vector3 doorDirection = (doorCollider.transform.position - transform.position).normalized;

            doorDirection.x *= ROOM_SCALE_X;
            doorDirection.y *= ROOM_SCALE_Y;

            Vector3 correspondingRoomPosition = transform.position + doorDirection;

            _doorRoomPositionDictionary.TryAdd(key, correspondingRoomPosition);
        }
    }

    private Vector3 GetScaledPosition(Node startingNode)
    {
        Vector3 pos = Node - startingNode;
        pos.x *= ROOM_SCALE_X;
        pos.y *= ROOM_SCALE_Y;

        return pos;
    }

    public Transform[] GetEnemySpawnPositions()
    {
        foreach (Transform child in transform)
        {
            if (child.name.Contains("Spawns"))
            {
                return child.GetComponentsInChildren<Transform>();
            }
        }

        return Array.Empty<Transform>();
    }

    [ContextMenu("LockRoom")]
    public void LockRoom()
    {
        if (_isLocked) return;

        foreach (Door door in _doorRoomPositionDictionary.Keys)
        {
            door.Lock();
        }

        _isLocked = true;
    }

    [ContextMenu("UnlockRoom")]
    public void UnlockRoom()
    {
        if (!_isLocked) return;

        foreach (Door door in _doorRoomPositionDictionary.Keys)
        {
            door.Unlock();
        }
        _isLocked = false;
    }

    public void Enter(Door door)
    {
        // broadcast entered room message
        FloorManager.OnRoomEntered?.Invoke(this, door);

        if (Cleared == false)
        {
            // spawn enemies
            if (EnemySpawner.Instance.SpawnEnemies(this))
            {
                // lock doors
                LockRoom();
            }
            else
            {
                Cleared = true;
                UnlockRoom();
            }

        }
    }

    public void Clear()
    {
        FloorManager.OnRoomCleared?.Invoke(this);
        Cleared = true;
        UnlockRoom();
    }
    public override string ToString()
    {
        return "Room: " + Node.ToString();
    }
}
