using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int watchAdBonusPoint = 500;

    private int scorePoints;
    private int rewardPoints;

    private void OnEnable()
    {
        GameEvents.OnGameStart += HandleResetScorePoints;
        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
        UIEvents.OnReviveButtonClicked += HandleDecreaseRewardPoints;
        UIEvents.OnUnlockShipButtonClicked += HandleDecreaseRewardPoints;
        UIEvents.OnWatchAdButtonClicked += HandleWatchAdButtonClicked;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= HandleResetScorePoints;
        GameEvents.OnEnemyDestroyed -= HandleEnemyDestroyed;
        UIEvents.OnReviveButtonClicked -= HandleDecreaseRewardPoints;
        UIEvents.OnUnlockShipButtonClicked -= HandleDecreaseRewardPoints;
        UIEvents.OnWatchAdButtonClicked -= HandleWatchAdButtonClicked;
    }

    private void Start()
    {
        rewardPoints = SaveData.LoadRewardPoints();
        UIEvents.RaiseRewardChanged(rewardPoints);
    }

    private void HandleResetScorePoints(bool isStarted)
    {
        if (!isStarted)
            return;

        scorePoints = 0;
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        IncreaseScorePoints(enemy);
        IncreaseRewardPoints(enemy);
    }

    private void IncreaseScorePoints(Enemy enemy)
    {
        if (!GameManager.Instance.IsGamePlayingState())
            return;

        scorePoints += enemy.ScorePoints;
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    private void IncreaseRewardPoints(Enemy enemy)
    {
        if (!GameManager.Instance.IsGamePlayingState())
            return;

        if (enemy.IsBoss)
        {
            rewardPoints += enemy.ScorePoints;
            UIEvents.RaiseRewardChanged(rewardPoints);
        }
    }

    private void HandleDecreaseRewardPoints(int cost)
    {
        rewardPoints -= cost;
        rewardPoints = Mathf.Clamp(rewardPoints, 0, rewardPoints);
        UIEvents.RaiseRewardChanged(rewardPoints);
        SaveData.SaveRewardPoints(rewardPoints);
    }

    private void HandleWatchAdButtonClicked()
    {
        rewardPoints += watchAdBonusPoint;
        UIEvents.RaiseRewardChanged(rewardPoints);
    }

    public int ScorePoints => scorePoints;
    public int RewardPoints => rewardPoints;
}
