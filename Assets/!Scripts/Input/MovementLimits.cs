using UnityEngine;

public class MovementLimits : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_mainCamera == null)
        {
            Debug.LogWarning("Main camera reference is null!");
            return;
        }

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, _mainCamera.ScreenToWorldPoint(Vector3.zero).x, _mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, _mainCamera.ScreenToWorldPoint(Vector3.zero).y, _mainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y);
        transform.position = clampedPosition;
    }
}