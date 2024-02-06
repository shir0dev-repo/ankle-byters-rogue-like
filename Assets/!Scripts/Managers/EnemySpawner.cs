using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    [SerializeField] GameObject winImage;
    
    bool winImageSpawned = false;

    float minX = -26f;
    float maxX = -9f;
    float minY = 0f;
    float maxY = 5f;

    GameObject bossInstance;
    Health bossHealth;

    private void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
        bossInstance = Instantiate(bossPrefab, new Vector3(-36f, 0f, 0f), Quaternion.identity);

        bossHealth = bossInstance.GetComponent<Health>();
    }

    private void Update()
    {
        if (bossHealth.CurrentHealth <= 0 && !winImageSpawned)
        {
            Debug.Log("Boss defeated!");
            if (winImage != null)
            {
                winImage.SetActive(true);
                
                Debug.Log("You win!");
                winImageSpawned = true;
            }
        }
    }
}
