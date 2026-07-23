using UnityEngine;

public class Projectile : Movement
{
    [SerializeField] private DamageDealer damageDealer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    public void ModifiyDamage(float buffPercent)
    {
        damageDealer.ModifiyDamage(buffPercent);
    }
}
