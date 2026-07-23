using UnityEngine;

[CreateAssetMenu(fileName = "Stats - ", menuName = "Scriptable Objects/Stats SO")]
public class StatsSO : ScriptableObject
{
    [SerializeField] private float projectileDamage;
    [SerializeField] private float collisionDamage;
    [SerializeField] private float defend;
    [SerializeField] private float maxHP;
    [SerializeField] private float moveSpeed;
    [Tooltip("Use for enemy")]
    [SerializeField] private int scorePoint;

    public float ProjectileDamage => projectileDamage;
    public float CollisionDamage => collisionDamage;
    public float Defend => defend;
    public float MaxHP => maxHP;
    public float MoveSpeed => moveSpeed;
    public int ScorePoint => scorePoint;
}
