using UnityEngine;

public class Item : Movement
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private ItemEffectSO itemEffectSO;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out var player))
        {
            ApplyEffect(player);
            Destroy(gameObject);
        }
    }

    public void SetupItem(Sprite sprite, ItemEffectSO itemEffectSO)
    {
        if (spriteRenderer == null)
            return;

        spriteRenderer.sprite = sprite;
        this.itemEffectSO = itemEffectSO;
    }

    public void ApplyEffect(Player player)
    {
        if (itemEffectSO == null)
        {
            Debug.Log("Item doesn't have effect");
            return;
        }

        itemEffectSO.ApplyEffect(player);
    }
}
