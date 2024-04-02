using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _destroyOnDeath = true;
    [SerializeField] private bool _hasIFrames = false;
    [SerializeField] private float _invincibilityDuration = 0.5f;
    private float _currentITimer;


    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }
    public DamageFlash damageFlash;

    public Action<int> OnHealthChanged { get; set; }
    public Action OnDeath { get; set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
        _currentITimer = _invincibilityDuration;
    }


    private void Update()
    {
        if (_currentITimer > 0)
            _currentITimer -= Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;
        else if (_hasIFrames && _currentITimer > 0) return;

        CurrentHealth -= damage;

        OnHealthChanged?.Invoke(CurrentHealth);

        if (damageFlash != null)
        {
            damageFlash.FlashStart();
        }

        _currentITimer = _invincibilityDuration;

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
