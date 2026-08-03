using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private int chanceToSpawnMultiBoss = 0;
    [SerializeField] private int chanceIncreaseInterval = 10;
    [SerializeField] private int maxChanceToSpawnBoss = 70;

    private WaitForSeconds waitTimeToSpawnWave;
    private Coroutine spawnEnemiesRoutine;
    private bool canSpawnEnemy;
    private bool isBossWave;
    private int currentWave;
    private List<EnemyBoss> bossAppearingList = new();

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

    private bool CanSpawnMultipleBoss()
    {
        float minChance = 10;
        float maxChance = 100;
        return Random.Range(minChance, maxChance) < chanceToSpawnMultiBoss;
    }

    private void SpawnEnemyBoss()
    {
        bool canSpawnMulti = CanSpawnMultipleBoss();
        int amountToSpawn = canSpawnMulti ? 2 : 1;
        StartCoroutine(SpawnEnemyBossRoutine(amountToSpawn));
    }

    private IEnumerator SpawnEnemyBossRoutine(int amountToSpawn)
    {
        for (int i = 0; i < amountToSpawn; i++)
        {
            EnemyBoss bossSelected = bossDB.Enemies[Random.Range(0, bossDB.Enemies.Length)] as EnemyBoss;
            EnemyBoss enemyBoss = Instantiate(bossSelected, bossWave.GetStartingPoint().position, Quaternion.identity);
            enemyBoss.Movement.SetupEnemyMove(bossWave);
            bossAppearingList.Add(enemyBoss);

            yield return waitTimeToSpawnWave;
        }
    }

    private void CheckIfBossDestroyed(Enemy gameObject)
    {
        if (gameObject == null || !gameObject.IsBoss)
            return;

        if (gameObject is EnemyBoss enemyBoss)
        {
            if (bossAppearingList.Contains(enemyBoss))
                bossAppearingList.Remove(enemyBoss);
        }

        if (bossAppearingList.Count > 0)
            return;

        bossAppearingList.Clear();
        isBossWave = false;
        chanceToSpawnMultiBoss += chanceIncreaseInterval;
        chanceToSpawnMultiBoss = Mathf.Clamp(chanceToSpawnMultiBoss, 0, maxChanceToSpawnBoss);
        Invoke(nameof(EnableSpawnWave), timeToSpawnWave);
    }

    private void HandleGameStart(bool isStarted)
    {
        if (isStarted)
        {
            currentWave = 0;
            chanceToSpawnMultiBoss = 0;
        }

        bossAppearingList = new();
        EnableSpawnWave();
    }

    private void HandleGameQuit()
    {
        DisableSpawnWave();

        foreach (var enemyBoss in bossAppearingList)
        {
            if (enemyBoss != null && enemyBoss.gameObject != null)
                enemyBoss.StopRoamingMove();
        }
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
