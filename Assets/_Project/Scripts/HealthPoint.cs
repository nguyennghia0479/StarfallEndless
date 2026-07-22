using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamageable
{
    public event Action OnDeath;

    [SerializeField] private int healthPoint = 100;

    private bool isDeath;

    public void TakeDamage(int damage)
    {
        healthPoint -= damage;
        if (healthPoint <= 0)
        {
            Die();
        }
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
