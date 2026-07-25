using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private float damage;
    private bool canDealDamage = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canDealDamage)
            return;

        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            GameEvents.RaiseHit(transform.position);
            damageable.TakeDamage(damage);
        }
    }

    public void Initialize(float damage) => this.damage = damage;
    public void EnableDealDamage() => canDealDamage = true;
    public void DisableDealDamage() => canDealDamage = false;
}
