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
    private float defaultFireRate;
    private bool isAutoFire;
    private float upgradeTimer;
    private float buffTimer;

    private const float MIN_FIRE_RATE = .25f;

    private void Update()
    {
        RemoveUpgradeProjectile();
        RemoveBuffFireRate();
    }

    public void Initialize(float projectileDamage)
    {
        this.projectileDamage = projectileDamage;
        defaultProjectileDamage = projectileDamage;

        defaultProjectile = projectilePrefab;
        defaultFireRate = fireRate;

        waitTime = new WaitForSeconds(fireRate);
        EnableAutoFire();
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
        upgradeTimer = duration;
        projectilePrefab = upgradeProjectile;
        projectileDamage = defaultProjectileDamage + (defaultProjectileDamage * buffPercent);
    }

    private void RemoveUpgradeProjectile()
    {
        if (projectilePrefab == defaultProjectile || projectileDamage == defaultProjectileDamage)
            return;

        upgradeTimer -= Time.deltaTime;
        if (upgradeTimer <= 0)
        {
            projectilePrefab = defaultProjectile;
            projectileDamage = defaultProjectileDamage;
        }
    }

    public void ApplyBuffFireRate(float buffFireRatePercent, float duration)
    {
        buffTimer = duration;
        fireRate = defaultFireRate - (defaultFireRate * buffFireRatePercent);
        fireRate = Mathf.Clamp(fireRate, MIN_FIRE_RATE, fireRate);
    }

    private void RemoveBuffFireRate()
    {
        if (fireRate == defaultFireRate)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
            fireRate = defaultFireRate;
    }
}
