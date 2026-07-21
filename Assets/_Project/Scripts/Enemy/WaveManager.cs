using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Wave settings")]
    [SerializeField] private WaveSO[] waves;
    [SerializeField] private EnemyListSO[] enemyList;
    [SerializeField] private float timeToSpawnWave = 2f;

    private bool canSpawn;
    private WaitForSeconds waitTimeToSpawnWave;
    private Coroutine spawnEnemiesRoutine;
    private Enemy enemyPrefab;

    private void Start()
    {
        waitTimeToSpawnWave = new WaitForSeconds(timeToSpawnWave);
        EnableSpawnEnemy();
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (canSpawn)
        {
            WaveSO waveSelected = waves[Random.Range(0, waves.Length)];
            EnemyListSO enemyListSelected = enemyList[Random.Range(0, enemyList.Length)];
            WaitForSeconds waitTimeSpawnEnemy = new(waveSelected.GetTimeToSpawnEnemy());

            for (int i = 0; i < enemyListSelected.Enemies.Length; i++)
            {
                if (!canSpawn)
                    break;

                enemyPrefab = enemyListSelected.Enemies[i];
                Enemy newEnemy = Instantiate(enemyPrefab, waveSelected.GetStartingPoint().position, Quaternion.identity);
                newEnemy.transform.Rotate(180, 0, 0);
                newEnemy.SetupEnemy(waveSelected);

                yield return waitTimeSpawnEnemy;
            }

            yield return waitTimeToSpawnWave;
        }
    }

    public void EnableSpawnEnemy()
    {
        if (canSpawn || spawnEnemiesRoutine != null)
            return;

        canSpawn = true;
        spawnEnemiesRoutine = StartCoroutine(SpawnEnemiesRoutine());
    }

    public void DisableSpawnEnemy()
    {
        canSpawn = false;

        if (spawnEnemiesRoutine != null)
        {
            StopCoroutine(spawnEnemiesRoutine);
            spawnEnemiesRoutine = null;
        }
    }
}
