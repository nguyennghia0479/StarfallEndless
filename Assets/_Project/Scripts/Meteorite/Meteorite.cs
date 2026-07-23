using UnityEngine;

public class Meteorite : Movement
{
    [SerializeField] private StatsSO stats;

    private HealthPoint health;
    private DamageDealer damageDealer;
    private float topBound;
    private bool isEnteredScreen;

    private void Awake()
    {
        health = GetComponent<HealthPoint>();
        damageDealer = GetComponent<DamageDealer>();

        Initialize(stats.MoveSpeed);
        health.Initialize(stats.MaxHP, stats.Defend);
        damageDealer.Initialize(stats.CollisionDamage);
    }

    private void OnEnable()
    {
        health.OnDestroyed += HandleDestroyed;
    }

    private void OnDisable()
    {
        health.OnDestroyed -= HandleDestroyed;
    }

    protected override void Update()
    {
        base.Update();

        if (transform.position.y <= topBound && !isEnteredScreen)
        {
            isEnteredScreen = true;
            health.EnableDamaged();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var _))
            Destroy(gameObject);
    }

    public void SetupMeteorite(float topBound)
    {
        this.topBound = topBound;
        health.DisableDamaged();
    }

    private void HandleDestroyed()
    {
        GameEvents.RaiseMeteoriteDestroyed(transform.position);
    }
}
