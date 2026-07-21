using UnityEngine;

[CreateAssetMenu(fileName = "Enemy ", menuName = "ScriptableObject/List of Enemy")]
public class EnemyListSO : ScriptableObject
{
    [SerializeField] private Enemy[] enemies;

    public Enemy[] Enemies => enemies;
}
