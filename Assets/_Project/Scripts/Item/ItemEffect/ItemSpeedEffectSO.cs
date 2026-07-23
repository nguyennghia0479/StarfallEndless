using UnityEngine;

[CreateAssetMenu(fileName = "Effect Speed - ", menuName = "Scriptable Objects/Item/Item Effect/Item Speed Effect")]
public class ItemSpeedEffectSO : ItemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float buffPercent = .1f;
    [SerializeField] private float duration = 10f;

    public override void ApplyEffect(Player player)
    {
        player.Movement.AddModifier(buffPercent, duration);
    }
}
