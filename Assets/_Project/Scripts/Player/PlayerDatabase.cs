using UnityEngine;

[CreateAssetMenu(fileName = "Player Ship Database", menuName = "Scriptable Objects/Database/Player Ship Database")]
public class PlayerDatabase : ScriptableObject
{
    [SerializeField] private PlayerShipSO[] playerShips;

    public PlayerShipSO[] Players => playerShips;
}
