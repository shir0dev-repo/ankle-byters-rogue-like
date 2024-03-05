using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
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

    private (Room room, List<BasicEnemy> enemies) _currentRoomEnemies;
    private bool _inCombat = false;

    public (Room room, List<BasicEnemy> enemies) CurrentRoomEnemies => _currentRoomEnemies;
    public bool InCombat => _inCombat;

    protected override void Awake()
    {
        base.Awake();

        _currentRoomEnemies.enemies = new List<BasicEnemy>();
    }

    private void Update()
    {
        if (_inCombat && _currentRoomEnemies.enemies.Count < 1)
        {
            _currentRoomEnemies.room.Clear();
            _currentRoomEnemies.room = null;

            _currentRoomEnemies.enemies.Clear();

            _inCombat = false;
        }
    }

    public void OnBossKilled()
    {
        //winImage.SetActive(true);
        //Debug.Log("You win!");
        //winImageSpawned = true;
    }

    public bool SpawnEnemies(Room room)
    {
        if (_currentRoomEnemies.enemies == null) _currentRoomEnemies = new();
        if (_currentRoomEnemies.enemies.Count > 0) return false;

        _currentRoomEnemies.room = room;
        _currentRoomEnemies.enemies = new List<BasicEnemy>();

        var spawnPositions = room.GetEnemySpawnPositions();

        foreach (var s in spawnPositions)
        {
            _currentRoomEnemies.enemies.Add(Instantiate(enemyPrefab, s.position, Quaternion.identity).GetComponent<BasicEnemy>());
        }

        _inCombat = _currentRoomEnemies.enemies.Count > 0;
        return _inCombat;
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