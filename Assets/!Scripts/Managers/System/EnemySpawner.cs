using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemySpawner : Singleton<EnemySpawner>
{
    [SerializeField] private List<GameObject> _spawnableEnemies = new();

    public GameObject bossPrefab;
    [SerializeField] GameObject door1;
    [SerializeField] GameObject door2;

    public bool bossSpawned = false;
    public bool bossDestroyed = false;

    float minX = -26f;
    float maxX = -12f;
    float minY = 0f;
    float maxY = 5f;

    float minDistanceToPlayer = 5f;

    private (Room room, List<BasicEnemy> enemies) _currentRoomEnemies;
    private (Room room, List<BossMovement> bosses) _currentRoomBosses;
    private bool _inCombat = false;

    public (Room room, List<BasicEnemy> enemies) CurrentRoomEnemies => _currentRoomEnemies;
    public (Room room, List<BossMovement> boss) CurrentRoomBosses => _currentRoomBosses;
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
    public bool SpawnBoss(Room room)
    {
        _currentRoomBosses.room = room;

        Transform[] spawnPositions = room.GetEnemySpawnPositions();

        var spawnPOsition = spawnPositions[Random.Range(0, spawnPositions.Length)];
        var bossInstance = Instantiate(bossPrefab, spawnPOsition.position, Quaternion.identity);
        _currentRoomBosses.bosses.Add(bossInstance.GetComponent<BossMovement>());
        return true;
    }
    public bool SpawnEnemies(Room room)
    {
        if (_currentRoomEnemies.enemies == null) _currentRoomEnemies = new();
        if (_currentRoomEnemies.enemies.Count > 0) return false;

        if (room.Node.NodeType == DungeonMaster2D.NodeType.Boss)
        {
            SpawnBoss(room);
            return true;
        }


        _currentRoomEnemies.room = room;
        _currentRoomEnemies.enemies = new List<BasicEnemy>();

        var spawnPositions = room.GetEnemySpawnPositions();

        foreach (var s in spawnPositions)
        {
            _currentRoomEnemies.enemies.Add(Instantiate(GetRandomEnemy(), s.position, Quaternion.identity).GetComponent<BasicEnemy>());
        }

        _inCombat = _currentRoomEnemies.enemies.Count > 0;
        return _inCombat;
    }

    private GameObject GetRandomEnemy()
    {
        return _spawnableEnemies[Random.Range(0, _spawnableEnemies.Count)];
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
}