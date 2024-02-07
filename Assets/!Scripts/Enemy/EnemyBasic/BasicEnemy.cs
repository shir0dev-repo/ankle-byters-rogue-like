using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float _speed;

    EnemySpawner enemySpawner;

    void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    void FixedUpdate()
    {
        Vector3 playerPosition = GameManager.Instance.PlayerManager.GetPlayerPosition();
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