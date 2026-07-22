using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] protected int scorePoints = 10;

    protected HealthPoint health;
    protected EnemyMovement movement;

    protected void Awake()
    {
        health = GetComponent<HealthPoint>();
        movement = GetComponent<EnemyMovement>();
    }

    protected void OnEnable()
    {
        health.OnDeath += HandleDeath;
    }

    protected void OnDisable()
    {
        health.OnDeath -= HandleDeath;
    }

    protected void HandleDeath()
    {
        GameEvents.RaiseEnemyDied(scorePoints);
    }

    public EnemyMovement Movement => movement;
}
