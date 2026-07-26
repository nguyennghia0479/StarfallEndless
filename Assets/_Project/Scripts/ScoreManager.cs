using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int scorePoints;
    private int rewardPoints;

    private void Start()
    {
        ResetPoints();
    }

    private void OnEnable()
    {
        GameEvents.OnGameRetry += ResetPoints;
        GameEvents.OnEnemyDestroyed += HandleEnemyDestroyed;
    }


    private void OnDisable()
    {
        GameEvents.OnGameRetry -= ResetPoints;
        GameEvents.OnEnemyDestroyed -= HandleEnemyDestroyed;
    }

    private void ResetPoints()
    {
        scorePoints = 0;
        rewardPoints = 0;
        UIEvents.RaiseRewardChanged(rewardPoints);
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    private void HandleEnemyDestroyed(Enemy enemy)
    {
        IncreaseRewardPoints(enemy);
        IncreaseScorePoints(enemy);
    }

    private void IncreaseRewardPoints(Enemy enemy)
    {
        if (enemy.IsBoss)
        {
            rewardPoints += enemy.ScorePoints;
            UIEvents.RaiseRewardChanged(rewardPoints);
        }
    }

    private void IncreaseScorePoints(Enemy enemy)
    {
        scorePoints += enemy.ScorePoints;
        UIEvents.RaiseScoreChanged(scorePoints);
    }

    public int ScorePoints => scorePoints;
    public int RewardPoints => rewardPoints;
}
