using UnityEngine;

public class EnemyBoss : Enemy
{
    private EnemyBossMovement bossMovement;

    protected override void Awake()
    {
        base.Awake();

        bossMovement = GetComponent<EnemyBossMovement>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        bossMovement.OnEnteredScreen += BossEnteredScreen;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        bossMovement.OnEnteredScreen -= BossEnteredScreen;
    }

    protected override void Start()
    {
        Health.Initialize(stats.MaxHP, stats.Defend);
        Shooter.Initialize(stats.ProjectileDamage, stats.Speed);
        DamageDealer.Initialize(stats.CollisionDamage);
        Movement.Initialize(stats.Speed);
        scorePoints = stats.ScorePoint;
        StopFire();
    }

    private void BossEnteredScreen()
    {
        StartFire();
    }

    protected override void HandlePlayerDestroyed()
    {
        base.HandlePlayerDestroyed();

        bossMovement.StopRoamingMove();
    }

    public void StopRoamingMove() => bossMovement.StopRoamingMove();
}
