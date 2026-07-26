using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected bool isBoss;

    public EnemyMovement Movement { get; private set; }

    protected int scorePoints;

    protected override void Awake()
    {
        base.Awake();

        Movement = GetComponent<EnemyMovement>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        GameEvents.OnPlayerDestroyed += HandlePlayerDestroyed;
    }

    protected override void OnDisable()
    {
        base.OnDisable();

        GameEvents.OnPlayerDestroyed -= HandlePlayerDestroyed;
    }

    protected override void Start()
    {
        base.Start();

        Movement.Initialize(stats.MoveSpeed);
        scorePoints = stats.ScorePoint;
        StartFire();
    }

    protected override void HandleDestroyed()
    {
        base.HandleDestroyed();

        GameEvents.RaiseEnemyDestroyed(this);
        Destroy(gameObject);
    }

    protected virtual void HandlePlayerDestroyed()
    {
        StopFire();
    }

    public int ScorePoints => scorePoints;
    public bool IsBoss => isBoss;
}
