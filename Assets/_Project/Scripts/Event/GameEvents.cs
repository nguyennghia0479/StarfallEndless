using System;
using UnityEngine;

public static class GameEvents
{
    // Gamestate Events
    public static event Action OnGameReady;
    public static event Action<bool> OnGameStart;
    public static event Action OnGameQuit;

    // Gameplay Events
    public static event Action OnPlayerDestroyed;
    public static event Action<Enemy> OnEnemyDestroyed;
    public static event Action<Vector2> OnMeteoriteDestroyed;
    public static event Action<GameObject> OnEntityDamaged;

    // VFX Events
    public static event Action<Vector2> OnHit;
    public static event Action<Vector2> OnExploded;
    public static event Action<GameObject> OnHealed;
    public static event Action<GameObject> OnConsumed;

    public static void RaiseGameReady()
    {
        OnGameReady?.Invoke();
    }

    public static void RaiseGameStart(bool isRestarted)
    {
        OnGameStart?.Invoke(isRestarted);
    }

    public static void RaiseGameQuit()
    {
        OnGameQuit?.Invoke();
    }

    public static void RaiseEnemyDestroyed(Enemy enemy)
    {
        OnEnemyDestroyed?.Invoke(enemy);
        OnExploded?.Invoke(enemy.gameObject.transform.position);
    }

    public static void RaisePlayerDestroyed(Vector2 position)
    {
        OnPlayerDestroyed?.Invoke();
        OnExploded?.Invoke(position);
    }

    public static void RaiseMeteoriteDestroyed(Vector2 position)
    {
        OnMeteoriteDestroyed?.Invoke(position);
        OnExploded?.Invoke(position);
    }

    public static void RaiseEntityDamaged(GameObject gameObject)
    {
        OnEntityDamaged?.Invoke(gameObject);
    }

    public static void RaiseHit(Vector2 position)
    {
        OnHit?.Invoke(position);
    }

    public static void RaiseHealedEffect(GameObject gameObject)
    {
        OnHealed?.Invoke(gameObject);
    }

    public static void RaiseConsumedEffect(GameObject gameObject)
    {
        OnConsumed?.Invoke(gameObject);
    }
}
