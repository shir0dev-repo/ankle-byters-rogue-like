using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _seekRange = 4f;

    EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.Instance.PlayerManager.GetPlayerPosition();
        float distanceToPlayer = Vector3.Distance(transform.position, playerPosition);
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);

    }

    void OnDestroy()
    {
        if (enemySpawner != null)
        {
            enemySpawner.EnemyDefeated();
        }
    }
}