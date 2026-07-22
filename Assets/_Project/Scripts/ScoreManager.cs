using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int scorePoints;

    private void OnEnable()
    {
        GameEvents.OnEnemyDied += IncreaseScorePoints;
        GameEvents.OnPlayerDied += ShowScorePoints;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDied -= IncreaseScorePoints;
        GameEvents.OnPlayerDied -= ShowScorePoints;
    }

    private void IncreaseScorePoints(int scorePoints)
    {
        this.scorePoints += scorePoints;
    }

    private void ShowScorePoints()
    {
        Debug.Log(scorePoints);
    }
}
