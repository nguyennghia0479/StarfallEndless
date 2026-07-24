using UnityEngine;

[CreateAssetMenu(fileName = "Effect Heal - ", menuName = "Scriptable Objects/Item/Item Effect/Item Heal Effect")]
public class ItemHealEffectSO : ItemEffectSO
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent = .2f;

    public override void ApplyEffect(Player player)
    {
        player.Health.ApplyHealing(healPercent);
        GameEvents.RaiseHealedEffect(player.gameObject);
    }
}
