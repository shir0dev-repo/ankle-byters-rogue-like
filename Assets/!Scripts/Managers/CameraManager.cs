using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const float _CAMERA_Z_PLANE = -10.0f;

    public static Vector2 ScreenExtents;

    private Camera _mainCamera;
    private void OnEnable()
    {
        FloorManager.OnRoomEntered += MoveToRoom;
    }

    private void OnDisable()
    {
        FloorManager.OnRoomEntered -= MoveToRoom;
    }

    private void Awake()
    {
        _mainCamera = Camera.main;

        ScreenExtents = new Vector2()
        {
            x = _mainCamera.orthographicSize * Screen.width / Screen.height,
            y = _mainCamera.orthographicSize,
        };
    }

    private void MoveToRoom(Room room, Door door)
    {
        Vector3 camPos = room.transform.position;
        camPos.z = transform.position.z;
        transform.position = camPos;
    }
}
