using UnityEngine;

[CreateAssetMenu(fileName = "Player Ship -", menuName = "Scriptable Objects/Player SO")]
public class PlayerShipSO : StatsSO
{
    [Header("Stats UI")]
    [SerializeField] private PlayerModel shipModel;
    [SerializeField] private int maxDamageRange = 10;
    [SerializeField] private int maxDefendRange = 10;
    [SerializeField] private int maxHPRange = 100;
    [SerializeField] private int maxSpeedRange = 10;

    [Header("Unlock")]
    [SerializeField] private bool unlockedByDefault;
    [SerializeField] private int unlockedCost = 500;

    public PlayerModel ShipModel => shipModel;
    public int MaxDamageRange => maxDamageRange;
    public int MaxDefendRange => maxDefendRange;
    public int MaxHPRange => maxHPRange;
    public int MaxSpeedRange => maxSpeedRange;
    public bool UnlockedByDefault => unlockedByDefault;
    public int UnlockedCost => unlockedCost;
}
