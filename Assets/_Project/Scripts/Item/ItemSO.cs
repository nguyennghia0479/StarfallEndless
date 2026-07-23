using UnityEngine;


[CreateAssetMenu(fileName = "Item - ", menuName = "Scriptable Objects/Item/Item SO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemEffectSO effect;

    public Sprite Icon => icon;
    public ItemEffectSO Effect => effect;
}
