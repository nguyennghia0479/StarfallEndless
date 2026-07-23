using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage = 10;

    private int defaultDamage;

    private void Awake()
    {
        defaultDamage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<IDamageable>(out var damageable))
        {
            GameEvents.RaiseExploded(transform.position, false);
            damageable.TakeDamage(damage);
        }
    }

    public void ModifiyDamage(float buffPercent)
    {
        if (buffPercent == 0)
        {
            damage = defaultDamage;
        }
        else
        {
            int damgeToAdd = Mathf.RoundToInt(defaultDamage * buffPercent);
            damage += damgeToAdd;
        }
    }
}
