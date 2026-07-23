using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float fireRate = .5f;

    private Projectile defaultProjectile;
    private Coroutine fireRoutine;
    private WaitForSeconds waitTime;
    private bool isAutoFire;
    private float buffTimer;

    private void Start()
    {
        defaultProjectile = projectilePrefab;
        waitTime = new WaitForSeconds(fireRate);
        EnableAutoFire();
    }

    private void Update()
    {
        RemoveModifier();
    }

    private IEnumerator FireRoutine()
    {
        while (isAutoFire)
        {
            foreach (Transform gunPoint in gunPoints)
            {
                ProjectileManager.Instance.CreateProjectile(projectilePrefab, gunPoint.position, gunPoint.rotation);
            }

            yield return waitTime;
        }
    }

    public void EnableAutoFire()
    {
        if (isAutoFire || fireRoutine != null) 
            return;

        isAutoFire = true;
        fireRoutine = StartCoroutine(FireRoutine());
    }
    public void DisableAutoFire()
    {
        isAutoFire = false;
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
        }
    }

    public void AddModifier(Projectile upgradeProjectile, float duration)
    {
        buffTimer = duration;
        projectilePrefab = upgradeProjectile;
    }

    private void RemoveModifier()
    {
        if (projectilePrefab == defaultProjectile)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
            projectilePrefab = defaultProjectile;
    }
}
