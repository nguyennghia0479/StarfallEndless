using UnityEngine;

[CreateAssetMenu(fileName = "Effect Speed - ", menuName = "Scriptable Objects/Item/Item Effect/Item Speed Effect")]
public class ItemSpeedEffectSO : ItemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float buffSpeedPercent = .3f;
    [Range(0f, .3f)]
    [SerializeField] private float buffFireRatePercent = .1f;
    [SerializeField] private float duration = 10f;

    public override void ApplyEffect(Player player)
    {
        player.Movement.AppylyBuffSpeed(buffSpeedPercent, duration);
        player.Shooter.ApplyBuffFireRate(buffFireRatePercent, duration);
        GameEvents.RaiseConsumedEffect(player.gameObject);
    }
}
