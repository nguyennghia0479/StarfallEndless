using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Transform projectileHolder;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform[] gunPoints;
    [SerializeField] private float fireRate = .5f;

    private Coroutine fireRoutine;
    private WaitForSeconds waitTime;
    private bool isAutoFire = true;

    private void Start()
    {
        waitTime = new WaitForSeconds(fireRate);
        fireRoutine = StartCoroutine(FireRoutine());
    }

    private IEnumerator FireRoutine()
    {
        while (isAutoFire)
        {
            foreach (Transform gunPoint in gunPoints)
            {
                Instantiate(projectilePrefab, gunPoint.position, gunPoint.rotation, projectileHolder);
            }

            yield return waitTime;
        }
    }

    public void EnableAutoFire()
    {
        if (isAutoFire || fireRoutine != null) return;

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
