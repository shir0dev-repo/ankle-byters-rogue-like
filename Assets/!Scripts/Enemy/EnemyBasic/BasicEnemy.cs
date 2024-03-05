using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    [SerializeField] float _speed;

    void FixedUpdate()
    {
        Vector3 playerPosition = PlayerManager.Instance.GetPlayerPosition();
        transform.position = Vector3.MoveTowards(transform.position, playerPosition, _speed * Time.deltaTime);
    }

    void OnDestroy()
    {
        if (EnemySpawner.Instance != null && EnemySpawner.Instance.CurrentRoomEnemies.enemies != null)
            EnemySpawner.Instance.CurrentRoomEnemies.enemies.Remove(this);
    }
}