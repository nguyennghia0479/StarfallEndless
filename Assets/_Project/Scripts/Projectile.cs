using UnityEngine;

public class Projectile : Movement
{
    [SerializeField] private DamageDealer damageDealer;
    [SerializeField] private PooledObject pooledObject;

    [Header("Mobile Adjust")]
    [SerializeField] private float adjustValue = .8f;
    private bool hasReturned;
    private float defaultMoveSpeed;

    private void Awake()
    {
        defaultMoveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReturnToPool();
    }

    protected override void SelfDestroy()
    {
        if (hasReturned)
            return;

        lifeTimer += Time.deltaTime;
        if (lifeTimer > lifeTime)
            ReturnToPool();
    }

    private void ReturnToPool()
    {
        if (hasReturned) return;

        if (pooledObject != null && pooledObject.Pool != null)
        {
            hasReturned = true;
            pooledObject.Pool.Release(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public override void Initialize(float damage)
    {
#if UNITY_ANDROID
        moveSpeed = defaultMoveSpeed;
        moveSpeed *= adjustValue;
#endif

        lifeTimer = 0;
        hasReturned = false;
        damageDealer.Initialize(damage);
    }
}
