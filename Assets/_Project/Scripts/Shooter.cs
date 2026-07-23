using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float fireRate = .5f;

    private Coroutine fireRoutine;
    private WaitForSeconds waitTime;
    private bool isAutoFire;
    private float buffPercent;
    private float buffTimer;

    private void Start()
    {
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
                Projectile newProjectile = ProjectileManager.Instance.CreateProjectile(projectilePrefab, gunPoint.position, gunPoint.rotation);
                newProjectile.ModifiyDamage(buffPercent);
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

    public void AddModifier(float buffPercent, float duration)
    {
        buffTimer = duration;
        this.buffPercent = buffPercent;
    }

    private void RemoveModifier()
    {
        if (buffPercent == 0)
            return;

        buffTimer -= Time.deltaTime;
        if (buffTimer <= 0)
            buffPercent = 0;
    }
}
