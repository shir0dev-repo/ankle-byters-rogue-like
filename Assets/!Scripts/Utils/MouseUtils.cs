using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class MouseUtils
{
    /// <summary>
    /// Calculates direction from transform to cursor.
    /// </summary>
    /// <param name="transform">The origin transform.</param>
    /// <returns>The vector from transform to cursor in world space.</returns>
    public static Vector3 DirectionToMouseWorldSpace(this Transform transform, bool normalized = true)
    {
        Camera mainCamera = Camera.main;
        
        Vector3 mousePositionScreen = Mouse.current.position.value;

        // ScreenToWorldPoint() returns the provided distance z from the camera plane, therefore by setting mousePosition.z to -camera.transform.position.z,
        // the calculation is negated and the returned z value will always be 0.
        mousePositionScreen.z = -mainCamera.transform.position.z;

        Vector3 direction = mainCamera.ScreenToWorldPoint(mousePositionScreen) - transform.position;

        if (normalized)
            direction.Normalize();

        return direction;
    }

    /// <summary>
    /// Calculates direction from transform to cursor.
    /// </summary>
    ///<param name = "transform" > The origin transform.</param>
    ///<param name="normalized">Return a normalized vector.</param>
    /// <returns>The vector from transform to cursor in screen space.</returns>
    public static Vector3 DirectionToMouseScreenSpace(this Transform transform, bool normalized = false)
    {
        Camera mainCamera = Camera.main;
        Vector3 mousePosition = Mouse.current.position.value;
        Vector3 objScreenPosition = transform.position;
        objScreenPosition.z = -mainCamera.transform.position.z;

        objScreenPosition = mainCamera.WorldToScreenPoint(objScreenPosition);

        Vector3 direction = mousePosition - objScreenPosition;
        
        if (normalized) direction.Normalize();
        
        return direction;
    }
}
