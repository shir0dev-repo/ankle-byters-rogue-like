using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    //Shadows not looking at the player
    List<GameObject> shadowPassive = new List<GameObject>();
    //Shadows looking at the player
    List<GameObject> shadowLooking = new List<GameObject>();
    void Start()
    {
        InsanityManager.OnInsanityChanged += ShadowBehaviour;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShadowBehaviour(int insanity)
    {
        
    }
}
