using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyPrefab2;

    public GameObject bossPrefab;
    //[SerializeField] GameObject winImage;
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    //bool winImageSpawned = false;
    public bool bossSpawned = false;
    public bool bossDestroyed = false;
    int numEnemies = 0;
    int maxEnemies = 5;

    float minX = -26f;
    float maxX = -12f;
    float minY = 0f;
    float maxY = 5f;

    float minDistanceToPlayer = 5f;

    GameObject bossInstance;
    public WinCheckCond WinCheckCond;
    Health bossHealth;

    private void Start()
    {
        if (door2 != null)
        {
            door2.SetActive(false);
        }
    }

    public void OnBossKilled()
    {
        //winImage.SetActive(true);
        //Debug.Log("You win!");
        //winImageSpawned = true;
    }
    //public void CheckWinCondition()
    //{
    //    Debug.LogWarning("Checking win condition!");
    //    if (bossDestroyed && !winImageSpawned)
    //    {
    //        winImage.SetActive(true);
    //        Debug.Log("You win!");
    //        winImageSpawned = true;
    //    }
    //}

    public void SpawnEnemies()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            numEnemies++;
            GameObject prefabToSpawn;

            if (Random.Range(0, 2) == 0)
            {
                prefabToSpawn = enemyPrefab;
            }
            else
            {
                prefabToSpawn = enemyPrefab2;
            }

            Vector3 randomPosition = GetRandomPosition();
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
        }
    }
    private Vector3 GetRandomPosition()
    {
        Vector3 randomPosition;
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        do
        {
            randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0f);
        } while (Vector3.Distance(randomPosition, playerPosition) < minDistanceToPlayer);

        return randomPosition;
    }

    public void SpawnBoss()
    {
        if (!bossSpawned)
        {
            bossInstance = Instantiate(bossPrefab, new Vector3(-36f, 0f, 0f), Quaternion.identity);
            bossHealth = bossInstance.GetComponent<Health>();
            bossHealth.OnDeath += OnBossKilled;
            bossSpawned = true;
            WinCheckCond.boss = bossInstance;
        }
    }

    // Method to be called when a mini enemy is defeated
    public void EnemyDefeated()
    {
        Debug.Log("An enemy was defeated!");
        numEnemies--;
        if (numEnemies <= 0)
        {
            Debug.Log("All mini enemies defeated!");
            // Activate the door
            if (door2 != null)
            {
                door2.SetActive(true);
                SpawnBoss();
            }
        }
    }
}