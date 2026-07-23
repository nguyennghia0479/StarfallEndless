using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Database", menuName = "Scriptable Objects/Enemy Database")]
public class EnemyDatabaseSO : ScriptableObject
{
    [SerializeField] private Enemy[] enemies;

    public Enemy[] Enemies => enemies;
}
