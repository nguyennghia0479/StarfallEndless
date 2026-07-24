using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int scorePoints;

    private void Awake()
    {
        GameEvents.RaiseScoreChanged(scorePoints);
    }

    private void OnEnable()
    {
        GameEvents.OnEnemyDestroyed += IncreaseScorePoints;
        GameEvents.OnPlayerDestroyed += ShowScorePoints;
    }

    private void OnDisable()
    {
        GameEvents.OnEnemyDestroyed -= IncreaseScorePoints;
        GameEvents.OnPlayerDestroyed -= ShowScorePoints;
    }

    private void IncreaseScorePoints(int scorePoints)
    {
        this.scorePoints += scorePoints;
        GameEvents.RaiseScoreChanged(this.scorePoints);
    }

    private void ShowScorePoints()
    {
        Debug.Log(scorePoints);
    }
}
