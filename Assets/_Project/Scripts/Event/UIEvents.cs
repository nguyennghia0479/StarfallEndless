using System;
using UnityEngine;

public static class UIEvents
{
    // Button Events
    public static event Action OnStartButtonClicked;
    public static event Action OnMainMenuButtonClicked;
    public static event Action<int> OnReviveButtonClicked;
    public static event Action OnEndGameButtonClicked;
    public static event Action<PlayerModel> OnSelectedShipButtonClicked;
    public static event Action<int> OnUnlockShipButtonClicked;

    // UI Events
    public static event Action<int> OnRewardChanged;
    public static event Action<int> OnScoreChanged;
    public static event Action<GameObject, float> OnHealthChanged;

    public static void RaiseStartButtonClicked()
    {
        OnStartButtonClicked?.Invoke();
    }

    public static void RaiseMainMenuButtonClicked()
    {
        OnMainMenuButtonClicked?.Invoke();
    }

    public static void RaiseReviveButtonClicked(int revivePointsAmount)
    {
        OnReviveButtonClicked?.Invoke(revivePointsAmount);
    }

    public static void RaiseEndGameButtonClicked()
    {
        OnEndGameButtonClicked.Invoke();
    }

    public static void RaiseSelectedShipButtonClicked(PlayerModel model)
    {
        OnSelectedShipButtonClicked?.Invoke(model);
    }

    public static void RaiseUnlockShipButton(int unlockedCost)
    {
        OnUnlockShipButtonClicked?.Invoke(unlockedCost);
    }

    public static void RaiseRewardChanged(int currentReward)
    {
        OnRewardChanged?.Invoke(currentReward);
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
