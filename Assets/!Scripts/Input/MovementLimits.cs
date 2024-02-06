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
        Vector3 minViewportBounds = _mainCamera.ViewportToWorldPoint(new Vector3(0, 0, _mainCamera.nearClipPlane));
        Vector3 maxViewportBounds = _mainCamera.ViewportToWorldPoint(new Vector3(1, 1, _mainCamera.nearClipPlane));

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, minViewportBounds.x, maxViewportBounds.x);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, minViewportBounds.y, maxViewportBounds.y);
        transform.position = clampedPosition;
    }
}