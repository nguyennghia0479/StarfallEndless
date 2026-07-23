using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<int> OnEnemyDied;
    public static event Action OnPlayerDied;
    public static event Action<Vector2, bool> OnExploded;
    public static event Action OnPlayerDamaged;
    public static event Action<Vector2> OnMeteoriteDestroyed;

    public static void RaiseEnemyDied(int scorePoints)
    {
        OnEnemyDied?.Invoke(scorePoints);
    }

    public static void RaisePlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public static void RaiseExploded(Vector2 position, bool isExploded = true)
    {
        OnExploded?.Invoke(position, isExploded);
    }

    public static void RaisePlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }

    public static void RaiseMeteoriteDestroyed(Vector2 position)
    {
        OnMeteoriteDestroyed?.Invoke(position);
    }
}
