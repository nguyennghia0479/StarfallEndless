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
    }

    private void OnDisable()
    {
        health.OnDeath -= HandleDeath;    
    }

    private void HandleDeath()
    {
        GameEvents.RaisePlayerDied();
    }
}
