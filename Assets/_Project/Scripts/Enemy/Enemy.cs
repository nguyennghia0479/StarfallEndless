using UnityEngine;

public class Enemy : Entity
{
    public EnemyMovement Movement {  get; private set; }
    protected int scorePoints;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<EnemyMovement>();
        Movement.Initialize(stats.MoveSpeed);
        scorePoints = stats.ScorePoint;
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();

        GameEvents.RaiseEnemyDied(scorePoints);
    }
}
