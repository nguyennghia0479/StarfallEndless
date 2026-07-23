using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class WaveManager : MonoBehaviour
{
    [Header("Enemy wave settings")]
    [SerializeField] private WaveSO[] waves;
    [SerializeField] private EnemyDatabaseSO[] enemyList;
    [SerializeField] private float timeToSpawnWave = 2f;
    
    [Header("Boss wave settings")]
    [SerializeField] private WaveSO bossWave;
    [SerializeField] private EnemyDatabaseSO bossList;

    private bool canSpawn;
    private WaitForSeconds waitTimeToSpawnWave;
    private Coroutine spawnEnemiesRoutine;

    private void Start()
    {
        waitTimeToSpawnWave = new WaitForSeconds(timeToSpawnWave);
        EnableSpawnEnemy();
    }

    private void Update()
    {
        if (Keyboard.current !=null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SpawnBossEnemy();
        }
    }

    private void SpawnBossEnemy()
    {
        DisableSpawnEnemy();

        EnemyBoss bossSelected = bossList.Enemies[Random.Range(0, bossList.Enemies.Length)] as EnemyBoss;
        EnemyBoss newBoss = Instantiate(bossSelected, bossWave.GetStartingPoint().position, Quaternion.identity);
        newBoss.Movement.SetupEnemyMove(bossWave);
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (canSpawn)
        {
            WaveSO waveSelected = waves[Random.Range(0, waves.Length)];
            EnemyDatabaseSO enemyListSelected = enemyList[Random.Range(0, enemyList.Length)];
            WaitForSeconds waitTimeSpawnEnemy = new(waveSelected.GetTimeToSpawnEnemy());

            for (int i = 0; i < enemyListSelected.Enemies.Length; i++)
            {
                if (!canSpawn)
                    break;

                Enemy enemyPrefab = enemyListSelected.Enemies[i];
                Enemy newEnemy = Instantiate(enemyPrefab, waveSelected.GetStartingPoint().position, Quaternion.identity);
                newEnemy.Movement.SetupEnemyMove(waveSelected);

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
