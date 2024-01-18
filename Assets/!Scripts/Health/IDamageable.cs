using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void TakeDamage(int damage);
    void Die();
    System.Action<int> OnHealthChanged { get; set; }
    System.Action OnDeath { get; set; }

}
