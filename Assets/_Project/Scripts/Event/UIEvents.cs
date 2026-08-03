using System;
using UnityEngine;

public static class UIEvents
{
    // Button Events
    public static event Action OnStartButtonClicked;
    public static event Action<bool> OnMainMenuButtonClicked;
    public static event Action<int> OnReviveButtonClicked;
    public static event Action OnEndGameButtonClicked;
    public static event Action<PlayerModel, PlayerShipSO> OnSelectedShipButtonClicked;
    public static event Action<int> OnUnlockShipButtonClicked;
    public static event Action OnButtonClicked;
    public static event Action OnSettingQuitButtonClicked;
    public static event Action OnQuitToGameOver;

    // UI Events
    public static event Action<int> OnRewardChanged;
    public static event Action<int> OnScoreChanged;
    public static event Action<GameObject, float, float> OnHealthChanged;
    public static event Action<string> OnLocaleChanaged;
    public static event Action OnLocaleChangeDone;

    public static void RaiseStartButtonClicked()
    {
        OnStartButtonClicked?.Invoke();
    }

    public static void RaiseMainMenuButtonClicked(bool canSave)
    {
        OnMainMenuButtonClicked?.Invoke(canSave);
    }

    public static void RaiseReviveButtonClicked(int revivePointsAmount)
    {
        OnReviveButtonClicked?.Invoke(revivePointsAmount);
    }

    public static void RaiseEndGameButtonClicked()
    {
        OnEndGameButtonClicked.Invoke();
    }

    public static void RaiseSelectedShipButtonClicked(PlayerModel model, PlayerShipSO shipSO)
    {
        OnSelectedShipButtonClicked?.Invoke(model, shipSO);
    }

    public static void RaiseUnlockShipButtonClicked(int unlockedCost)
    {
        OnUnlockShipButtonClicked?.Invoke(unlockedCost);
    }

    public static void RaiseButtonClicked()
    {
        OnButtonClicked?.Invoke();
    }

    public static void RaiseSettingQuitButtonClicked()
    {
        OnSettingQuitButtonClicked?.Invoke();
    }

    public static void RaiseQuitToGameOver()
    {
        OnQuitToGameOver?.Invoke();
    }

    public static void RaiseRewardChanged(int currentReward)
    {
        OnRewardChanged?.Invoke(currentReward);
    }

    public static void RaiseScoreChanged(int currentScore)
    {
        OnScoreChanged?.Invoke(currentScore);
    }

    public static void RaiseHealthChanged(GameObject gameObject, float maxHP, float currentHP)
    {
        OnHealthChanged?.Invoke(gameObject, maxHP, currentHP);
    }

    public static void RaiseLocaleChanged(string localeCode)
    {
        OnLocaleChanaged?.Invoke(localeCode);
    }

    public static void RaiseLocaleChangeDone()
    {
        OnLocaleChangeDone?.Invoke();
    }
}
