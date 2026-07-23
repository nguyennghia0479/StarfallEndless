using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private ItemDatabaseSO itemDB;
    [SerializeField] private Item itemPrefab;

    private ItemSO[] itemsSO;

    private void Awake()
    {
        itemsSO = itemDB.ItemsSO;
    }

    private void OnEnable()
    {
        GameEvents.OnMeteoriteDestroyed += DropItem;
    }

    private void OnDisable()
    {
        GameEvents.OnMeteoriteDestroyed -= DropItem;
    }

    private void DropItem(Vector2 position)
    {
        ItemSO itemSO = itemsSO[Random.Range(0, itemsSO.Length)];
        Item newItem = Instantiate(itemPrefab, position, Quaternion.identity);
        newItem.SetupItem(itemSO.Icon, itemSO.Effect);
    }
}
