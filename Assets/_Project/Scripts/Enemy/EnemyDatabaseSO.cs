using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Database", menuName = "Scriptable Objects/Database/Enemy Database")]
public class EnemyDatabaseSO : ScriptableObject
{
    [SerializeField] private Enemy[] enemies;

    public Enemy[] Enemies => enemies;
}
