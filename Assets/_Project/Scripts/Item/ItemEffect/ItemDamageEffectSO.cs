using UnityEngine;

[CreateAssetMenu(fileName = "Effect Damage - ", menuName = "Scriptable Objects/Item/Item Effect/Item Damage Effect")]
public class ItemDamageEffectSO : ItemEffectSO
{
    [SerializeField] private Projectile upgradeProjectile;
    [Range(0f, 1f)]
    [SerializeField] private float buffPercent = .3f;
    [SerializeField] private float duration = 10;

    public override void ApplyEffect(Player player)
    {
        player.Shooter.ApplyUpgradeProjectile(upgradeProjectile, buffPercent, duration);
    }
}
