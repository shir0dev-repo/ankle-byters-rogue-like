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
    void Start()
    {
        InsanityManager.OnInsanityChanged += ShadowBehaviour;
        SpawnShadows();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShadowBehaviour(int insanity)
    {
        
    }

    void SpawnShadows()
    {
        Vector3 testSpawn = new Vector3(-8f, -3f);
        for (int i = 0; i < 10; i++)
        {
            Instantiate<GameObject>(shadowPrefab, testSpawn, Quaternion.identity, transform);
            testSpawn = new Vector3(testSpawn.x + 1.5f, -3f);
        }
    }
}
