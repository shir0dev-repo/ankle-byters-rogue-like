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
    void Start()
    {
        InsanityManager.OnInsanityChanged += ShadowBehaviour;
        //SpawnShadows();
        SpawnShadowCluster();
        SpawnShadowCluster(3);
        SpawnShadowCluster(8);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShadowBehaviour(int insanity)
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
                shadowLooking.Add(shadowPassive[0]);
                shadowPassive.RemoveAt(0);
                lookerRatio = ((shadowLooking.Count / (float)(shadowLooking.Count + shadowPassive.Count)) * 10);
            }
        }
        else if (lookerRatio > stage)
        {
            while (lookerRatio > stage)
            {
                shadowPassive.Add(shadowLooking[0]);
                shadowLooking.RemoveAt(0);
                lookerRatio = ((shadowLooking.Count / (float)(shadowLooking.Count + shadowPassive.Count)) * 10);
            }
        }
        UpdateLookers();
    }

    void UpdateLookers()
    {
        foreach (GameObject looker in shadowLooking)
        {
            looker.GetComponent<ShadowScript>().MakeLooker();
        }
        foreach (GameObject passive in shadowPassive)
        {
            passive.GetComponent<ShadowScript>().MakePassive();
        }
    }

    void SpawnShadows()
    {
        Vector3 testSpawn = new Vector3(-8f, -3f);
        for (int i = 0; i < 10; i++)
        {
            shadowPassive.Add(Instantiate(shadowPrefab, testSpawn, Quaternion.identity, transform));
            testSpawn = new Vector3(testSpawn.x + 1.5f, -3f);
        }
    }

    void SpawnShadowCluster(int cluserSize = 6)
    {
        if (cluserSize == 0)
        {
            Debug.LogWarning("Cluster Size shouldn't be zero");
            return;
        }
        float spawnAngles = 360 / (cluserSize - 1);
        Vector3 randomSpawn = new Vector3(Random.Range(-7.0f, 7.0f), Random.Range(-3.0f, 3.0f));
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
