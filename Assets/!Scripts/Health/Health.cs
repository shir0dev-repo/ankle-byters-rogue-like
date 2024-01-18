using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private bool _useDebugging;
    public int MaxHealth => _maxHealth;
    public int CurrentHealth { get; private set; }

    private void Awake()
    {
        CurrentHealth = _maxHealth;
    }

    // uncomment to test damage taking
    //private void Update()
    //{
    //    if (Keyboard.current.spaceKey.wasPressedThisFrame)
    //        TakeDamage(3);
    //}

    public void TakeDamage(int damage)
    {
        if (damage <= 0) return;

        Debug.Log($"{gameObject.name} took {damage} damage!");
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            Die();
    }

    public void Die()
    {
        Debug.Log("Goodbye cruel world.");
    }
}
