using UnityEngine;

[CreateAssetMenu(fileName = "Item Database", menuName = "Scriptable Objects/Database/Item Database")]
public class ItemDatabaseSO : ScriptableObject
{
    [SerializeField] private ItemSO[] itemsSO;

    public ItemSO[] ItemsSO => itemsSO;
}
