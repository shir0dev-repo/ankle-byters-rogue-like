using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    //Shadows not looking at the player
    List<GameObject> shadowPassive = new List<GameObject>();
    //Shadows looking at the player
    List<GameObject> shadowLooking = new List<GameObject>();

    [SerializeField] GameObject shadowPrefab;
    int _insanityStage;
    private Vector3 _roomPosition = new Vector3(0, 0);
    private Bounds _roomBounds;
    void Start()
    {
        InsanityManager.OnInsanityChanged += CurrentInsanityStage;
        FloorManager.OnRoomEntered += PlaceShadows;
        //SpawnShadows();
        SpawnShadowCluster();
        SpawnShadowCluster(3);
        SpawnShadowCluster(8);
        _roomBounds = new Bounds(_roomPosition, new Vector3(14, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlaceShadows(Room room, Door door)
    {
        foreach(GameObject shadow in shadowPassive)
        {
            Destroy(shadow);
        }
        shadowPassive.Clear();
        foreach(GameObject shadow in shadowLooking)
        {
            Destroy(shadow);
        }
        shadowLooking.Clear();
        _roomPosition = room.transform.position;
        _roomBounds = new Bounds(_roomPosition, new Vector3(14, 6));
        for (int i = 0; i <= Random.Range(1, 5); i++)
        {
            SpawnShadowCluster(Random.Range(2, 7));
        }
        InsanityStage(_insanityStage);
    }

    
    //Note: Code used to see _roomBounds, code was copy and pasted from unity's Renderer.bounds page and slightly modified
    // code has no gameplay purpose and can be deleted, if you're reading this then I forgot to delete it myself
    public void OnDrawGizmosSelected()
    {
        if (_roomBounds == null)
            return;
        Bounds bounds = _roomBounds;
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }

    void CurrentInsanityStage(int insanity)
    {
        //10 Insanity 'stages' for every 10 percent of the insanity filled 10 percent of the shadows will begin looking at the player
        int insanityStage = insanity / 10;
        if (insanityStage != _insanityStage)
        {
            _insanityStage = insanityStage;
            Debug.Log(_insanityStage);
            InsanityStage(_insanityStage);
        }

    }

    void InsanityStage(int stage)
    {
        if (stage == 10)
        {
            //all attack player
            return;
        }
        // Calculate the ratio between Lookers and Passives,
        float lookerRatio = ((shadowLooking.Count / (float)(shadowLooking.Count + shadowPassive.Count)) * 10);

        if (lookerRatio < stage)
        {
            while(lookerRatio < stage)
            {
                int randomPassive = Random.Range(0, shadowPassive.Count);
                shadowLooking.Add(shadowPassive[randomPassive]);
                shadowPassive.RemoveAt(randomPassive);
                lookerRatio = ((shadowLooking.Count / (float)(shadowLooking.Count + shadowPassive.Count)) * 10);
            }
        }
        else if (lookerRatio > stage)
        {
            while (lookerRatio > stage)
            {
                int randomLooker = Random.Range(0, shadowLooking.Count);
                shadowPassive.Add(shadowLooking[randomLooker]);
                shadowLooking.RemoveAt(randomLooker);
                lookerRatio = ((shadowLooking.Count / (float)(shadowLooking.Count + shadowPassive.Count)) * 10);
            }
        }
        SetLookerState();
    }

    void SetLookerState()
    {
        if (shadowLooking != null)
        {
            foreach (GameObject looker in shadowLooking)
            {
                looker.GetComponentInChildren<ShadowScript>().MakeLooker();
            }
        }
        if (shadowPassive != null)
        {
            foreach (GameObject passive in shadowPassive)
            {
                passive.GetComponentInChildren<ShadowScript>().MakePassive();
            }
        }
    }

    Vector3 GetRandomSpawn()
    {
        Vector3 randomSpawn;
        int randomSpawnIndex = 0;
        do
        {
            randomSpawn = new Vector3(_roomPosition.x + Random.Range(-7.0f, 7.0f), _roomPosition.y + Random.Range(-3.0f, 3.0f));
            randomSpawnIndex++;
        } while (_roomBounds.Contains(randomSpawn));
        Debug.Log(randomSpawnIndex);
        return randomSpawn;
    }

    void SpawnShadowCluster(int cluserSize = 6)
    {
        if (cluserSize == 0)
        {
            Debug.LogWarning("Cluster Size shouldn't be zero");
            return;
        }
        float spawnAngles = 360 / (cluserSize - 1);
        Vector3 randomSpawn = GetRandomSpawn();
        // Quaternion.Euler(0, 0, Random.Range(0, 359)) * vector3.up // How to rotate vector randomly, comment for now will add later
        // Makes a cluster of 3 shadows not just a straight line
        if (cluserSize == 3)
        {
            spawnAngles = 360 / cluserSize;
            for (int i = 0; i < cluserSize; i++)
            {
                float spawnRad = (spawnAngles * i) * Mathf.Deg2Rad;
                float newX = Mathf.Cos(spawnRad);
                float newY = Mathf.Sin(spawnRad);
                Vector3 circleSpawn = new Vector3((newX + randomSpawn.x) + Random.Range(-0.5f, 0.5f), (newY + randomSpawn.y) + Random.Range(-0.5f, 0.5f));
                shadowPassive.Add(Instantiate(shadowPrefab, circleSpawn, Quaternion.identity, transform));
            }
            return;
        }
        shadowPassive.Add(Instantiate(shadowPrefab, randomSpawn, Quaternion.identity, transform));
        for(int i = 0; i < cluserSize -1; i++)
        {
            float spawnRad = (spawnAngles * i) * Mathf.Deg2Rad;
            float newX = Mathf.Cos(spawnRad);
            float newY = Mathf.Sin(spawnRad);
            Vector3 circleSpawn = new Vector3((newX + randomSpawn.x) + Random.Range(-0.5f, 0.5f), (newY + randomSpawn.y) + Random.Range(-0.5f, 0.5f));
            shadowPassive.Add(Instantiate(shadowPrefab, circleSpawn, Quaternion.identity, transform));
        }

    }
}
