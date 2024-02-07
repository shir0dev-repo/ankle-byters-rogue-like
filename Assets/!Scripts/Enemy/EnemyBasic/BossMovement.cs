using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public Transform player;

    [SerializeField] public float seekDistance = 10f;

    private float speed = 5;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        Seek();
    }

    public void Seek()
    {
        if (player != null)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Check if player is within seek distance
            if (Vector3.Distance(transform.position, player.position) <= seekDistance)
            {
                // Rotation on y axis instead of z
                if (directionToPlayer.x < 0)
                {
                    transform.localScale = new Vector3(1f, 1f, 1f);
                }
                else
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }

                // Move towards player
                transform.Translate(directionToPlayer * speed * Time.deltaTime);
            }
        }
    }

    void OnDestroy()
    {
        if (enemySpawner != null)
        {
            enemySpawner.OnBossKilled();
        }
    }
}