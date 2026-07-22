using UnityEngine;

public class Player : MonoBehaviour
{
    private HealthPoint health;

    private void Awake()
    {
        health = GetComponent<HealthPoint>();
    }

    private void OnEnable()
    {
        health.OnDeath += HandleDeath;
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;
        health.OnDamaged -= HandleDamaged;
    }

    private void HandleDeath()
    {
        GameEvents.RaiseExploded(transform.position);
        GameEvents.RaisePlayerDied();
    }

    private void HandleDamaged()
    {
        GameEvents.RaisePlayerDamaged();
    }
}
