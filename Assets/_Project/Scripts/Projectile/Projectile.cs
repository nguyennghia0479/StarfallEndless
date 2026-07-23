using UnityEngine;

public class Projectile : Movement
{
    private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            GameEvents.RaiseExploded(transform.position, false);
            damageable.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public override void Initialize(float damage) => this.damage = damage;
}
