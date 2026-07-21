using System.Collections;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float fireRate = .5f;

    private Coroutine fireRoutine;
    private WaitForSeconds waitTime;
    private bool isAutoFire;

    protected virtual void Start()
    {
        waitTime = new WaitForSeconds(fireRate);
        EnableAutoFire();
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
}
