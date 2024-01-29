using System.Collections.Generic;
using UnityEngine;

public class HealthHeartSystem : MonoBehaviour
{
    public GameObject heartPrefab;
    public Health health;
    List<HealthHearts> hearts = new List<HealthHearts>();

    private void OnEnable()
    {
        GameManager.Instance.Player.TryGetComponent(out health);

        health.OnHealthChanged += UpdateHearts;
    }
    private void Start()
    {
        DrawHearts();
    }
    private void UpdateHearts(int currentHealth)
    {
        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = Mathf.Clamp(health.CurrentHealth - (i * 2), 0, 2);
            hearts[i].SetHeartImage((HeartStatus)heartStatusRemainder);
        }
    }
    private void OnDisable()
    {
        health.OnHealthChanged -= UpdateHearts;
    }
    public void DrawHearts()
    {
        ClearHearts();

        //Determine how many hearts to make total, based on max health
        int maxHealthRemainder = health.MaxHealth % 2;
        int heartsToMake = (health.MaxHealth / 2) + maxHealthRemainder;
        for (int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

        UpdateHearts(health.MaxHealth);
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
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHearts>();
    }
}
