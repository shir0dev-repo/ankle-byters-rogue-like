using UnityEditor.Overlays;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject player;
    public GameObject door;
    public Camera mainCamera;

    public Vector3 playerPosition;
    public Vector3 doorLocation;
    public Vector3 cameraLocation;
    void Start()
    {
        if (player != null && door != null)
        {
            doorLocation = door.transform.position;
        }
        else
        {
            Debug.LogError("Player or door not assigned to the Door script! Assign both in the Unity Editor.");
        }
    }

    void Update()
    {
        if(mainCamera != null)
        {
            cameraLocation = mainCamera.transform.position;
        }
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("A New Room!");

            if (cameraLocation.x < doorLocation.x)
            {
                Debug.Log("Welcome to Room 2");
                Camera.main.transform.position = new Vector3(13.4f, 0f, -1f);
            }
            else
            {
                Debug.Log("Welcome to Room 1");
                Camera.main.transform.position = new Vector3(0f, 0f, -1f);
            }
        }
    }
}
