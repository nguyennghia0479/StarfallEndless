using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            GameEvents.RaiseHit(transform.position);
            damageable.TakeDamage(damage);
        }
    }

    public void Initialize(float damage) => this.damage = damage;
}
