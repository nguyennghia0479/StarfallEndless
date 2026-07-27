using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int scorePoints;
    private int rewardPoints;

    private void OnEnable()
    {
        GameEvents.OnGameStart += HandleResetPoints;
        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
        UIEvents.OnReviveButtonClicked += HandleDecreaseRewardPoints;
        UIEvents.OnUnlockShipButtonClicked += HandleDecreaseRewardPoints;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= HandleResetPoints;
        GameEvents.OnEnemyDestroyed -= HandleEnemyDestroyed;
        UIEvents.OnReviveButtonClicked -= HandleDecreaseRewardPoints;
        UIEvents.OnUnlockShipButtonClicked -= HandleDecreaseRewardPoints;
    }

    private void Start()
    {
        // Get RewardPoints from PlayerPreft.GetInt
        rewardPoints = 1000;
        UIEvents.RaiseRewardChanged(rewardPoints);
    }

    private void HandleResetPoints(bool isRestarted)
    {
        if (!isRestarted)
            return;

        scorePoints = 0;
        rewardPoints = 0;
        UIEvents.RaiseRewardChanged(rewardPoints);
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        IncreaseScorePoints(enemy);
        IncreaseRewardPoints(enemy);
    }

    private void IncreaseScorePoints(Enemy enemy)
    {
        scorePoints += enemy.ScorePoints;
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    private void IncreaseRewardPoints(Enemy enemy)
    {
        if (enemy.IsBoss)
        {
            rewardPoints += enemy.ScorePoints;
            UIEvents.RaiseRewardChanged(rewardPoints);
        }
    }

    private void HandleDecreaseRewardPoints(int revivePointsAmount)
    {
        rewardPoints -= revivePointsAmount;
        rewardPoints = Mathf.Clamp(rewardPoints, 0, rewardPoints);
        UIEvents.RaiseRewardChanged(rewardPoints);
    }   

    public int ScorePoints => scorePoints;
    public int RewardPoints => rewardPoints;
}
