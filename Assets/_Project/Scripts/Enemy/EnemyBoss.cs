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
        base.Start();

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
