using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamageable
{
    public event Action OnDestroyed;
    public event Action OnDamaged;

    private const int DEFEND_MITTIGATION = 50;

    private float maxHP;
    private float currentHP;
    private float defend;
    private float defaultDefend;
    private bool isDeath;
    private bool canTakeDamge;
    private float buffTimer;

    private void Update()
    {
        RemoveIncreaseDefend();
    }

    public void Initialize(float maxHP, float defend)
    {
        this.maxHP = maxHP;
        this.defend = defend;
        currentHP = maxHP;
        defaultDefend = defend;
        EnableDamaged();
    }

    public void TakeDamage(float rawDamage)
    {
        if (isDeath || !canTakeDamge) return;

        float mitigationPercent = defend / (defend + DEFEND_MITTIGATION);
        float finalDamage = rawDamage * (1 - mitigationPercent);
        finalDamage = Mathf.Clamp(finalDamage, 1, finalDamage);

        currentHP -= finalDamage;
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

    public void ApplyHealing(float healPercent)
    {
        currentHP += maxHP * healPercent;
        currentHP = Mathf.Clamp(currentHP, 0, maxHP);
    }

    public void ApplyIncreaseDefend(float buffPercent, float duration)
    {
        buffTimer = duration;
        defend = defaultDefend + (defaultDefend * buffPercent);
    }

    private void RemoveIncreaseDefend()
    {
        if (defend == defaultDefend)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
            defend = defaultDefend;
    }

    public void DisableDamaged() => canTakeDamge = false;
    public void EnableDamaged() => canTakeDamge = true;
}
