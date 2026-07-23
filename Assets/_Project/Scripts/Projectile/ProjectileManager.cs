using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public static ProjectileManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public Projectile CreateProjectile(Projectile projectilePrefab, Vector3 position, Quaternion rotation)
    {
        return Instantiate(projectilePrefab, position, rotation, transform);
    }
}
