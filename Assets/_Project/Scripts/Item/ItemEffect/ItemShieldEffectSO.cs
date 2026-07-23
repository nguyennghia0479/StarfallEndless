using UnityEngine;

[CreateAssetMenu(fileName = "Effect Shield - ", menuName = "Scriptable Objects/Item/Item Effect/Item Shield Effect")]
public class ItemShieldEffectSO : ItemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float shieldDefPercent = .3f;
    [SerializeField] private float duration = 10f;
    [SerializeField] private Sprite shieldSprite;

    public override void ApplyEffect(Player player)
    {
        player.EnableShield(shieldSprite, duration);
        player.Health.AddModifier(shieldDefPercent, duration);
    }
}
