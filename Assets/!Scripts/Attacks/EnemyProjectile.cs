using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _lifetime = 5f;

    private Vector3 directionToPlayer;

    private void Awake()
    {
        directionToPlayer = (PlayerManager.Instance.GetPlayerPosition() - transform.position).normalized;

        Destroy(gameObject, _lifetime);
    }

    private void FixedUpdate()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(directionToPlayer * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
