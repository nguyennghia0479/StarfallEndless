using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamageable
{
    public event Action OnDestroyed;
    public event Action OnDamaged;

    [SerializeField] private int maxHP = 100;

    [SerializeField] private int currentHP;
    private float shieldDefPercent;
    private bool isDeath;
    private bool canTakeDamge;
    private float buffTimer;

    private void Awake()
    {
        currentHP = maxHP;
        EnableDamaged();
    }

    private void Update()
    {
        RemoveModifier();
    }

    public void HealingHealth(float healPercent)
    {
        int healAmount = Mathf.RoundToInt(maxHP * healPercent);
        currentHP += healAmount;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void TakeDamage(int damage)
    {
        if (isDeath || !canTakeDamge) return;

        damage -= Mathf.RoundToInt(shieldDefPercent * damage);
        currentHP -= damage;
        OnDamaged?.Invoke();

        if (currentHP <= 0)
            Destroyed();
    }

    private void Destroyed()
    {
        if (isDeath)
            return;

        isDeath = true;
        OnDestroyed?.Invoke();
        Destroy(gameObject);
    }

    public void AddModifier(float shieldDefPercent, float duration)
    {
        buffTimer = duration;
        this.shieldDefPercent = shieldDefPercent;
    }

    private void RemoveModifier()
    {
        if (shieldDefPercent == 0)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
            shieldDefPercent = 0;
    }

    public void DisableDamaged() => canTakeDamge = false;
    public void EnableDamaged() => canTakeDamge = true;
}
