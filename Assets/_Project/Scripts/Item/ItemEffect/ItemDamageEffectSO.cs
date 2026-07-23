using UnityEngine;

[CreateAssetMenu(fileName = "Effect Damage - ", menuName = "Scriptable Objects/Item/Item Effect/Item Damage Effect")]
public class ItemDamageEffectSO : ItemEffectSO
{
    [SerializeField] private Projectile upgradeProjectile;
    [SerializeField] private float duration = 10;

    public override void ApplyEffect(Player player)
    {
        player.Shooter.AddModifier(upgradeProjectile, duration);
    }
}
