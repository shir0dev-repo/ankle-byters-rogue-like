using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject bossPrefab;
    [SerializeField] GameObject winImage;
    [SerializeField] GameObject door;

    bool winImageSpawned = false;
    int numEnemies = 5; 

    float minX = -26f;
    float maxX = -12f;
    float minY = 0f;
    float maxY = 5f;

    GameObject bossInstance;
    Health bossHealth;

    private void Start()
    {
        SpawnEnemies();

        if (door != null)
        {
            door.SetActive(false);
        }
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

    void SpawnEnemies()
    {
        for (int i = 0; i < numEnemies; i++)
        {
            Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
            Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        }
        bossInstance = Instantiate(bossPrefab, new Vector3(-36f, 0f, 0f), Quaternion.identity);
        bossHealth = bossInstance.GetComponent<Health>();
    }

    // Method to be called when a mini enemy is defeated
    public void EnemyDefeated()
    {
        numEnemies--;
        if (numEnemies <= 0)
        {
            Debug.Log("All mini enemies defeated!");
            // Activate the door
            if (door != null)
            {
                door.SetActive(true);
            }
        }
    }
}