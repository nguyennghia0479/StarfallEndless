using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamageable
{
    public event Action OnDeath;
    public event Action OnDamaged;

    [SerializeField] private int healthPoint = 100;

    private bool isDeath;

    public void TakeDamage(int damage)
    {
        if (isDeath) return;

        healthPoint -= damage;
        OnDamaged?.Invoke();

        if (healthPoint <= 0)
            Die();
    }

    private void Die()
    {
        if (isDeath)
            return;

        isDeath = true;
        OnDeath?.Invoke();
        Destroy(gameObject);
    }
}
