using System;
using UnityEngine;

public class HealthPoint : MonoBehaviour, IDamageable
{
    public event Action OnDestroyed;

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
        RemoveBuffDefend();
    }

    public void Initialize(float maxHP, float defend)
    {
        this.maxHP = maxHP;
        this.defend = defend;
        currentHP = maxHP;
        defaultDefend = defend;
        EnableDamaged();
        GameEvents.RaiseHealthChanged(gameObject, currentHP);
    }

    public void TakeDamage(float rawDamage)
    {
        if (isDeath || !canTakeDamge) return;
        float finalDamage = CalculateFinalDamage(rawDamage);

        currentHP -= finalDamage;
        GameEvents.RaiseEntityDamaged(gameObject);
        GameEvents.RaiseHealthChanged(gameObject, currentHP);

        if (currentHP <= 0)
            Destroyed();
    }

    private float CalculateFinalDamage(float rawDamage)
    {
        float mitigationPercent = defend / (defend + DEFEND_MITTIGATION);
        float finalDamage = rawDamage * (1 - mitigationPercent);
        finalDamage = Mathf.Clamp(finalDamage, 1, finalDamage);

        return finalDamage;
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

    public void ApplyBuffDefend(float buffPercent, float duration)
    {
        buffTimer = duration;
        defend = defaultDefend + (defaultDefend * buffPercent);
    }

    private void RemoveBuffDefend()
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
