using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerMeleeAttack : MonoBehaviour
{
    private float timeBtwnAttack;
    public float startTimeBtwnAttack;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemyToAttack;
    public int damage;

    private const string _MELEE_ATTACK_ACTION_NAME = "MeleeAttack";
    private InputAction _meleeAttack;
    private PlayerInputHandler _playerInputHandler;

    private void Awake()
    {
        _playerInputHandler = GetComponent<PlayerInputHandler>();
        _meleeAttack = _playerInputHandler.PlayerActions.FindAction(_MELEE_ATTACK_ACTION_NAME);
    }
    private void Update()
    {
        if(timeBtwnAttack <= 0)
        {
            if(Input.GetMouseButton(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemyToAttack);
                for (int i = 0; i < enemiesToDamage.Length; i++)
                {
                    Destroy(enemiesToDamage[i].gameObject);
                    //Fix for damage instead of destroy
                }
            }
            timeBtwnAttack = startTimeBtwnAttack;
        }
        else
        {
            timeBtwnAttack -= Time.deltaTime;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}
