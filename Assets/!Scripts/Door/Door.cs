using UnityEditor.Overlays;
using UnityEngine;

public class Door : MonoBehaviour
{
    //instead of storing the player, use a layermask for it
    [SerializeField] private LayerMask _playerLayermask;

    public Vector3 GetAdjacentRoom()
    {
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.z = 0;
        
        Vector3 directionFromCamera = (transform.position - cameraPos).normalized;

        return Vector3.Scale((Vector3)CameraManager.ScreenExtents, directionFromCamera) * 2f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_playerLayermask.IsLayer(collision.gameObject.layer)) return;
        
        GameManager.Instance.CameraManager.EnterRoom(this);
    }
}
