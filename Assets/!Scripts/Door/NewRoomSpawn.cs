using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRoomSpawn : MonoBehaviour
{
    public GameObject player;
    public GameObject door;

    [SerializeField] public Vector3 playerPosition;
    public Vector3 doorLocation;

    void Start()
    {
        if (player != null && door != null)
        {
            doorLocation = door.transform.position;
        }
    }
    void Update()
    {
        if (player != null)
        {
            playerPosition = player.transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
         Camera mainCamera = Camera.main;
             if (mainCamera != null )
             {
                if (playerPosition.x < doorLocation.x)
                {
                    transform.position = new Vector3(13.4f, 0f, 0f);
                }
                else
                {
                    transform.position = new Vector3(0f, 0f, 0f);
                }
            }
        
        }
    }
}
