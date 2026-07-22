using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<int> OnEnemyDied;
    public static event Action OnPlayerDied;

    public static void RaiseEnemyDied(int scorePoints)
    {
        OnEnemyDied?.Invoke(scorePoints);
    }

    public static void RaisePlayerDied()
    {
        OnPlayerDied?.Invoke();
    }
}
