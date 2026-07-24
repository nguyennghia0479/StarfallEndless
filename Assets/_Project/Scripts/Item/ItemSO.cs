using UnityEngine;


[CreateAssetMenu(fileName = "Item - ", menuName = "Scriptable Objects/Item/Items")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private Sprite icon;
    [SerializeField] private ItemEffectSO effect;

    public Sprite Icon => icon;
    public ItemEffectSO Effect => effect;
}
