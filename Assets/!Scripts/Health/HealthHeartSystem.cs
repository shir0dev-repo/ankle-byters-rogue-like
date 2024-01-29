using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

public class HealthHeartSystem : MonoBehaviour
{
    public GameObject heartPrefab;
    public Health health;
    List<HealthHearts> hearts = new List<HealthHearts>();

    private void OnEnable()
    {
        if (health != null)
        {
            health.OnHealthChanged += UpdateHearts;
            Debug.Log("Subscribed to OnHealthChanged event.");
        }
        else
        {
            Debug.LogWarning("Health component is not assigned.");
        }
    }
    private void Start()
    {
        DrawHearts();
    }
    private void UpdateHearts(int currentHealth)
    {
        Debug.Log("Updating hearts...");
        DrawHearts();
    }
    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHearts;
        Debug.Log("Unsubscribed from OnHealthChanged event.");
    }
    public void DrawHearts()
    {
        ClearHearts();

        //Determine how many hearts to make total, based on max health
        float maxHealthRemainder = health.MaxHealth % 2;
        int heartsToMake = (int)((health.MaxHealth / 2) + maxHealthRemainder);
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }
        for(int i = 0; i <hearts.Count; i++)
        {
            int heartStatusRemainer = (int)Mathf.Clamp(health.MaxHealth - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainer);
        }
    }

    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHearts heartComponent = newHeart.GetComponent<HealthHearts>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHearts>();
    }
}
