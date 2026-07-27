using System.Collections;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance { get; private set; }

    [Header("Enemy wave settings")]
    [SerializeField] private WaveSO[] waves;
    [SerializeField] private EnemyDatabaseSO[] enemyDB;
    [SerializeField] private float timeToSpawnWave = 2f;

    [Header("Boss wave settings")]
    [SerializeField] private WaveSO bossWave;
    [SerializeField] private EnemyDatabaseSO bossDB;
    [SerializeField] private int waveAmountToSpawnBoss = 10;

    private WaitForSeconds waitTimeToSpawnWave;
    private Coroutine spawnEnemiesRoutine;
    private EnemyBoss enemyBoss;
    private bool canSpawnEnemy;
    private bool isBossWave;
    private int currentWave;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        waitTimeToSpawnWave = new WaitForSeconds(timeToSpawnWave);
    }

    private void OnEnable()
    {
        GameEvents.OnGameStart += HandleGameStart;
        GameEvents.OnGameQuit += HandleGameQuit;
        GameEvents.OnPlayerDestroyed += DisableSpawnWave;
        GameEvents.OnEnemyDestroyed += CheckIfBossDestroyed;
    }

    private void OnDisable()
    {
        GameEvents.OnGameStart -= HandleGameStart;
        GameEvents.OnGameQuit -= HandleGameQuit;
        GameEvents.OnPlayerDestroyed -= DisableSpawnWave;
        GameEvents.OnEnemyDestroyed -= CheckIfBossDestroyed;
    }

    private void CheckIfBossDestroyed(Enemy gameObject)
    {
        if (gameObject.IsBoss)
        {
            isBossWave = false;
            EnableSpawnWave();
        }
    }

    private void SpawnEnemyBoss()
    {
        EnemyBoss bossSelected = bossDB.Enemies[Random.Range(0, bossDB.Enemies.Length)] as EnemyBoss;
        enemyBoss = Instantiate(bossSelected, bossWave.GetStartingPoint().position, Quaternion.identity);
        enemyBoss.Movement.SetupEnemyMove(bossWave);
    }

    private IEnumerator SpawnWaveRoutine()
    {
        while (canSpawnEnemy)
        {
            WaveSO waveSelected = waves[Random.Range(0, waves.Length)];
            EnemyDatabaseSO enemyListSelected = enemyDB[Random.Range(0, enemyDB.Length)];
            WaitForSeconds waitTimeSpawnEnemy = new(waveSelected.GetTimeToSpawnEnemy());

            yield return SpawnEnemyRoutine(waveSelected, enemyListSelected, waitTimeSpawnEnemy);
            yield return waitTimeToSpawnWave;
            CheckToSpawnEnemyBoss();
        }
    }

    private IEnumerator SpawnEnemyRoutine(WaveSO waveSelected, EnemyDatabaseSO enemyListSelected, WaitForSeconds waitTimeSpawnEnemy)
    {
        for (int i = 0; i < enemyListSelected.Enemies.Length; i++)
        {
            if (!canSpawnEnemy)
                break;

            Enemy enemyPrefab = enemyListSelected.Enemies[i];
            Enemy newEnemy = Instantiate(enemyPrefab, waveSelected.GetStartingPoint().position, Quaternion.identity);
            newEnemy.Movement.SetupEnemyMove(waveSelected);

            yield return waitTimeSpawnEnemy;
        }
    }

    private void CheckToSpawnEnemyBoss()
    {
        currentWave++;
        if (currentWave % waveAmountToSpawnBoss == 0 && !isBossWave)
        {
            isBossWave = true;
            DisableSpawnWave();
            SpawnEnemyBoss();
        }
    }

    private void HandleGameStart(bool isRestarted)
    {
        if (isRestarted)
            currentWave = 0;

        EnableSpawnWave();
    }

    private void HandleGameQuit()
    {
        DisableSpawnWave();

        if (enemyBoss != null && enemyBoss.gameObject != null)
            enemyBoss.StopRoamingMove();
    }

    private void EnableSpawnWave()
    {
        if (canSpawnEnemy || spawnEnemiesRoutine != null)
            return;

        isBossWave = false;
        canSpawnEnemy = true;
        spawnEnemiesRoutine = StartCoroutine(SpawnWaveRoutine());
    }

    private void DisableSpawnWave()
    {
        canSpawnEnemy = false;

        if (spawnEnemiesRoutine != null)
        {
            StopCoroutine(spawnEnemiesRoutine);
            spawnEnemiesRoutine = null;
        }
    }

    public int CurrentWave => currentWave;
}
