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
    private float projectileDamage;
    private float defaultProjectileDamage;
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
        RemoveUpgradeProjectile();
    }

    public void Initialize(float projectileDamage)
    {
        this.projectileDamage = projectileDamage;
        defaultProjectileDamage = projectileDamage;
    }

    private IEnumerator FireRoutine()
    {
        while (isAutoFire)
        {
            foreach (Transform gunPoint in gunPoints)
            {
                Projectile newProjectile = ProjectileManager.Instance.CreateProjectile(projectilePrefab, gunPoint.position, gunPoint.rotation);
                newProjectile.Initialize(projectileDamage);
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

    public void ApplyUpgradeProjectile(Projectile upgradeProjectile, float buffPercent, float duration)
    {
        buffTimer = duration;
        projectilePrefab = upgradeProjectile;
        projectileDamage = defaultProjectileDamage + (defaultProjectileDamage * buffPercent);
    }

    private void RemoveUpgradeProjectile()
    {
        if (projectilePrefab == defaultProjectile || projectileDamage == defaultProjectileDamage)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
        {
            projectilePrefab = defaultProjectile;
            projectileDamage = defaultProjectileDamage;
        }
    }
}
