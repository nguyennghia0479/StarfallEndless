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

    public void CreateProjectile(GameObject projectilePrefab, Vector3 position, Quaternion rotation)
    {
        Instantiate(projectilePrefab, position, rotation, transform);
    }
}
