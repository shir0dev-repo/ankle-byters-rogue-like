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

        Debug.Log($"{gameObject.name} took {damage} damage!");
        CurrentHealth -= damage;

        OnHealthChanged?.Invoke(CurrentHealth);

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Goodbye cruel world.");
        if (_destroyOnDeath)
            Destroy(gameObject);
    }
}
