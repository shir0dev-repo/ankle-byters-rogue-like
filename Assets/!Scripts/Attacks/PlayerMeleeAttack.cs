using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMeleeAttack : MonoBehaviour
{
    [SerializeField] float _fireCooldown = 0.3f;
    [SerializeField] private int _damage = 2;

    private const string _MELEE_ATTACK_ACTION_NAME = "MeleeAttack";
    private InputAction _meleeAttack;
    private PlayerInputHandler _playerInputHandler;

    private float _nextFireTime;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyToAttack;


    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _meleeAttack = _playerInputHandler.PlayerActions.FindAction(_MELEE_ATTACK_ACTION_NAME);
    }
    private void Update()
    {
        if (_meleeAttack.ReadValue<float>() > 0.5f && Time.time >= _nextFireTime)
        {
            Attack();
            _nextFireTime = Time.time + _fireCooldown;
        }
    }
    private void Attack()
    {
        Collider2D[] enemiesInAttackRange = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyToAttack);

        foreach (Collider2D enemyCollider in enemiesInAttackRange)
        {
            Health enemyHealth = enemyCollider.GetComponent<Health>();

            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
