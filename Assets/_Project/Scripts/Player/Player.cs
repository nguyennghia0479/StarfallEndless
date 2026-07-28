using UnityEngine;

public class Player : Entity
{
    [SerializeField] private Transform startPoint;

    public PlayerMovement Movement { get; private set; }
    public PlayerVisual Visual { get; private set; }

    private Collider2D collider;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<PlayerMovement>();
        Visual = GetComponent<PlayerVisual>();
        collider = GetComponent<Collider2D>();
    }

    protected override void Start()
    {
        base.Start();

        Movement.Initialize(stats.MoveSpeed);
        DisablePlayer();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameEvents.OnGameReady += EnablePlayer;
        GameEvents.OnGameStart += ActivatePlayer;
        UIEvents.OnReviveButtonClicked += HandlePlayerRevive;
        GameEvents.OnGameQuit += DisablePlayer;
        UIEvents.OnSelectedShipButtonClicked += HandleShipVisuals;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GameEvents.OnGameReady -= EnablePlayer;
        GameEvents.OnGameStart -= ActivatePlayer;
        UIEvents.OnReviveButtonClicked -= HandlePlayerRevive;
        GameEvents.OnGameQuit -= DisablePlayer;
        UIEvents.OnSelectedShipButtonClicked -= HandleShipVisuals;
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();

        GameEvents.RaisePlayerDestroyed(transform.position);
        DisablePlayer();
    }

    public void UpdatePlayerStats(PlayerShipSO playerShipSO)
    {
        stats = playerShipSO;
        Health.Initialize(stats.MaxHP, stats.Defend);
        Shooter.Initialize(stats.ProjectileDamage);
        DamageDealer.Initialize(stats.CollisionDamage);
        Movement.Initialize(stats.MoveSpeed);
    }

    private void HandlePlayerRevive(int _)
    {
        EnablePlayer();
    }

    private void ActivatePlayer(bool _)
    {
        StartFire();
        Visual.StopBlinkEffect();
        Health.EnableDamaged();
        collider.enabled = true;
    }

    private void HandleShipVisuals(PlayerModel model)
    {
        Shooter.SetupGunPoints(model.GunPoints);
    }

    private void EnablePlayer()
    {
        transform.position = startPoint.localPosition;
        Visual.EnableShipVisual();
        Visual.PlayBlinkEffect();
        Movement.EnableMovement();
        DamageDealer.EnableDealDamage();
        Health.ResetHealth();
    }

    private void DisablePlayer()
    {
        Visual.DisableShipVisual();
        Visual.HideShield();
        Shooter.DisableAutoFire();
        Shooter.StopAllBuffs();
        Movement.DisableMovement();
        Movement.StopBuff();
        DamageDealer.DisableDealDamage();
        Health.DisableDamaged();
        collider.enabled = false;
    }
}
