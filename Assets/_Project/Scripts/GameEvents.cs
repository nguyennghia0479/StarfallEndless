using System;
using UnityEngine;

public static class GameEvents
{
    // Gameplay Events
    public static event Action<int> OnEnemyDestroyed;
    public static event Action OnPlayerDestroyed;
    public static event Action<GameObject> OnEntityDamaged;
    public static event Action<Vector2> OnMeteoriteDestroyed;

    // VFX Events
    public static event Action<Vector2> OnHit;
    public static event Action<Vector2> OnExploded;
    public static event Action<GameObject> OnHealed;
    public static event Action<GameObject> OnConsumed;

    // UI Events
    public static event Action<int> OnScoreChanged;
    public static event Action<GameObject, float> OnHealthChanged;

    public static void RaiseEnemyDestroyed(int scorePoints, Vector2 position)
    {
        OnEnemyDestroyed?.Invoke(scorePoints);
        OnExploded?.Invoke(position);
    }

    public static void RaisePlayerDestroyed(Vector2 position)
    {
        OnPlayerDestroyed?.Invoke();
        OnExploded?.Invoke(position);
    }

    public static void RaiseHit(Vector2 position)
    {
        OnHit?.Invoke(position);
    }

    public static void RaiseEntityDamaged(GameObject gameObject)
    {
        OnEntityDamaged?.Invoke(gameObject);
    }

    public static void RaiseMeteoriteDestroyed(Vector2 position)
    {
        OnMeteoriteDestroyed?.Invoke(position);
    }

    public static void RaiseHealedEffect(GameObject gameObject)
    {
        OnHealed?.Invoke(gameObject);
    }

    public static void RaiseConsumedEffect(GameObject gameObject)
    {
        OnConsumed?.Invoke(gameObject);
    }

    public static void RaiseScoreChanged(int currentScore)
    {
        OnScoreChanged?.Invoke(currentScore);
    }

    public static void RaiseHealthChanged(GameObject gameObject, float currentHP)
    {
        OnHealthChanged?.Invoke(gameObject, currentHP);
    }
}
