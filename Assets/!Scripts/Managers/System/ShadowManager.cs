using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : Singleton<ShadowManager>
{
    //Shadows not looking at the player
    List<GameObject> shadowPassive = new List<GameObject>();
    //Shadows looking at the player
    List<GameObject> shadowLooking = new List<GameObject>();

    List<Vector2> shadowPosition = new List<Vector2>();

    [SerializeField] GameObject shadowPrefab;
    [SerializeField] GameObject EdgeColliderObject;
    int _insanityStage;
    [SerializeField] EdgeCollider2D edgeCollider;
    private Vector3 _roomPosition = new Vector3(0, 0);
    int spawnLoop = 0;
    void Start()
    {
        InsanityManager.OnInsanityChanged += CurrentInsanityStage;
        FloorManager.OnRoomEntered += PlaceShadows;
    }
    public void PlaceShadows()
    {
        foreach (GameObject shadow in shadowPassive)
        {
            Destroy(shadow);
        }
        shadowPassive.Clear();
        foreach (GameObject shadow in shadowLooking)
        {
            Destroy(shadow);
        }
        shadowLooking.Clear();
        shadowPosition.Clear();
        _roomPosition = new Vector3(0, 0);
        edgeCollider.offset = _roomPosition;
        for (int i = 0; i < edgeCollider.pointCount; i++)
        {
            edgeCollider.points[i] += (Vector2)_roomPosition;
        }
        //edgeCollider.offset = _roomPosition;
        for (int i = 0; i <= Random.Range(3, 6); i++)
        {
            SpawnShadowCluster(Random.Range(1, 5));
        }
        InsanityStage(_insanityStage);
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
        shadowPosition.Clear();
        _roomPosition = room.transform.position;
        edgeCollider.offset = _roomPosition;
        for(int i = 0; i < edgeCollider.pointCount; i++)
        {
            edgeCollider.points[i] += (Vector2)_roomPosition;
        }
        //edgeCollider.offset = _roomPosition;
        for (int i = 0; i <= Random.Range(3, 6); i++)
        {
            SpawnShadowCluster(Random.Range(1, 5));
        }
        InsanityStage(_insanityStage);
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

    Vector3 GenerateSpawn()
    {
        spawnLoop++;
        Vector3 fullRandom = new Vector3(_roomPosition.x + Random.Range(-9.0f, 9.0f), _roomPosition.y + Random.Range(-5.0f, 5.0f));
        Vector3 randomSpawn = edgeCollider.ClosestPoint(fullRandom);
        if (shadowPosition == null || spawnLoop >= 15)
            return randomSpawn;
        foreach (Vector2 shadowPoint in shadowPosition)
        {
            if (Vector3.Distance(shadowPoint, randomSpawn) <= 5.0f)
            {
                GenerateSpawn();
            }
        }
        return randomSpawn;
    }

    void SpawnShadowCluster(int cluserSize = 6)
    {
        spawnLoop = 0;
        if (cluserSize == 0)
        {
            Debug.LogWarning("Cluster Size shouldn't be zero");
            return;
        }
        //Debug.Log($"SpawnShadowCluster() Room Position, {_roomPosition.x}X, {_roomPosition.y}Y");
        Vector3 randomSpawn = GenerateSpawn();
        shadowPosition.Add(randomSpawn);
        //Debug.Log($"Spawning shadow at {randomSpawn.x}X, {randomSpawn.y}Y, rolled spawn {fullRandom.x}X, {fullRandom.y}Y");
        if (cluserSize == 1)
        {
            shadowPassive.Add(Instantiate(shadowPrefab, transform.TransformPoint(randomSpawn), Quaternion.identity, transform));
            return;
        }
        float spawnAngles = 360 / (cluserSize - 1);
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
            shadowPassive.Add(Instantiate(shadowPrefab, edgeCollider.ClosestPoint(circleSpawn), Quaternion.identity, transform));
        }

    }
}
