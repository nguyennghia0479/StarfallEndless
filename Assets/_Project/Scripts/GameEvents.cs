using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<int> OnEnemyDied;
    public static event Action OnPlayerDied;
    public static event Action<Vector2> OnExploded;
    public static event Action OnPlayerDamaged;

    public static void RaiseEnemyDied(int scorePoints)
    {
        OnEnemyDied?.Invoke(scorePoints);
    }

    public static void RaisePlayerDied()
    {
        OnPlayerDied?.Invoke();
    }

    public static void RaiseExploded(Vector2 position)
    {
        OnExploded?.Invoke(position);
    }

    public static void RaisePlayerDamaged()
    {
        OnPlayerDamaged?.Invoke();
    }
}
