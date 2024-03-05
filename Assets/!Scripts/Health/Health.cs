using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _destroyOnDeath = true;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }

    public Action<int> OnHealthChanged { get; set; }
    public Action OnDeath { get; set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        CurrentHealth -= damage;

        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        OnDeath?.Invoke();

        if (_destroyOnDeath)
            Destroy(gameObject);
    }
}
