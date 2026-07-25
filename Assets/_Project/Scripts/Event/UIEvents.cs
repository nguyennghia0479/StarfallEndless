using System;
using UnityEngine;

public static class UIEvents
{
    public static event Action<int> OnScoreChanged;
    public static event Action<GameObject, float> OnHealthChanged;
    public static event Action OnPlayerRevived;

    public static void RaiseScoreChanged(int currentScore)
    {
        OnScoreChanged?.Invoke(currentScore);
    }

    public static void RaiseHealthChanged(GameObject gameObject, float currentHP)
    {
        OnHealthChanged?.Invoke(gameObject, currentHP);
    }

    public static void RaisePlayerRevived()
    {
        OnPlayerRevived?.Invoke();
    }
}
