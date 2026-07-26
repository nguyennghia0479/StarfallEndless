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

        GameEvents.OnGameStarted += OnGameStarted;
        GameEvents.OnGameRetried += OnGameRetried;
        UIEvents.OnPlayerRevived += EnablePlayer;
        UIEvents.OnGamePlayed += EnablePlayer;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GameEvents.OnGameStarted -= OnGameStarted;
        GameEvents.OnGameRetried -= OnGameRetried;
        UIEvents.OnPlayerRevived -= EnablePlayer;
        UIEvents.OnGamePlayed -= EnablePlayer;
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();

        GameEvents.RaisePlayerDestroyed(transform.position);
        DisablePlayer();
    }

    private void OnGameStarted()
    {
        StartFire();
        Visual.StopBlinkEffect();
        Health.EnableDamaged();
        collider.enabled = true;
    }

    private void OnGameRetried(bool isRetried)
    {
        if (isRetried)
            EnablePlayer();
        else
            DisablePlayer();
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
