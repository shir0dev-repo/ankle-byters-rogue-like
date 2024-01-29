using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private const float _CAMERA_Z_PLANE = -10.0f;

    public static Vector2 ScreenExtents;

    private Camera _mainCamera;
    private Vector3 _currentRoomCenter;

    private void Awake()
    {
        _mainCamera = Camera.main;

        ScreenExtents = new Vector2()
        {
            x = _mainCamera.orthographicSize * Screen.width / Screen.height,
            y = _mainCamera.orthographicSize,
        };
    }

    public void EnterRoom(Door door)
    {
        _currentRoomCenter += door.GetAdjacentRoom();
        Debug.Log(_currentRoomCenter);
        _mainCamera.transform.position = new Vector3()
        {
            x = _currentRoomCenter.x,
            y = _currentRoomCenter.y,
            z = _CAMERA_Z_PLANE
        };
    }
}
