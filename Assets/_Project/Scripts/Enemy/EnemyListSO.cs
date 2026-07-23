using UnityEngine;

[CreateAssetMenu(fileName = "Enemy ", menuName = "Scriptable Objects/List of Enemy")]
public class EnemyListSO : ScriptableObject
{
    [SerializeField] private Enemy[] enemies;

    public Enemy[] Enemies => enemies;
}
