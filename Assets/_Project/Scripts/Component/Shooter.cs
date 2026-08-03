using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float rawFireRate = .6f;
    [SerializeField] private float fireRateFactor = .02f;

    private GameObject defaultProjectile;
    private Coroutine fireRoutine;
    private WaitForSeconds waitTime;
    private float projectileDamage;
    private float defaultProjectileDamage;
    private float fireRate;
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

    public void Initialize(float projectileDamage, float speed)
    {
        this.projectileDamage = projectileDamage;
        defaultProjectileDamage = projectileDamage;
        defaultProjectile = projectilePrefab;

        fireRate = rawFireRate - (speed * fireRateFactor);
        fireRate = Mathf.Clamp(fireRate, MIN_FIRE_RATE, fireRate);
        defaultFireRate = fireRate;
        
        waitTime = new WaitForSeconds(fireRate);
    }

    private IEnumerator FireRoutine()
    {
        while (isAutoFire)
        {
            foreach (Transform gunPoint in gunPoints)
            {
                GameObject projectilePool = ObjectPoolManager.Instance.GetPool(projectilePrefab, gunPoint.position, gunPoint.rotation);
                if (projectilePool.TryGetComponent<Projectile>(out var projectile))
                    projectile.Initialize(projectileDamage);
            }
            GameEvents.RaiseOnShooted(transform.position);

            yield return waitTime;
        }
    }

    public void SetupGunPoints(Transform[] gunPoints)
    {
        this.gunPoints = gunPoints;
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

    public void StopAllBuffs()
    {
        upgradeTimer = 0;
        buffTimer = 0;
    }

    public void ApplyUpgradeProjectile(GameObject projectileUpgrade, float buffPercent, float duration)
    {
        upgradeTimer = duration;
        projectilePrefab = projectileUpgrade;
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
