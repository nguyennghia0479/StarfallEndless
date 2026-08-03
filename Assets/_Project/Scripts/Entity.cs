using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] protected StatsSO stats;

    public HealthPoint Health { get; private set; }
    public Shooter Shooter { get; private set; }
    public DamageDealer DamageDealer { get; private set; }

    protected virtual void Awake()
    {
        Health = GetComponent<HealthPoint>();
        Shooter = GetComponent<Shooter>();
        DamageDealer = GetComponent<DamageDealer>();
    }

    protected virtual void Start()
    {
        Health.Initialize(stats.MaxHP, stats.Defend);
        Shooter.Initialize(stats.ProjectileDamage, stats.Speed);
        DamageDealer.Initialize(stats.CollisionDamage);
    }

    protected virtual void OnEnable()
    {
        Health.OnDestroyed += HandleDestroyed;
    }

    protected virtual void OnDisable()
    {
        Health.OnDestroyed -= HandleDestroyed;
    }

    protected virtual void HandleDestroyed()
    {

    }

    protected void StartFire() => Shooter.EnableAutoFire();

    protected void StopFire() => Shooter.DisableAutoFire();
}
