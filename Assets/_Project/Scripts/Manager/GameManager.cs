using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    MainMenu, GamePlaying, GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private PlayerDatabase playerDB;

    private Dictionary<int, bool> playerDict;
    private GameState currentState = GameState.MainMenu;
    private int enemiesKill;
    private int bossesKill;
    private int rewardPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        SetupPlayerDatabase();
    }

    private void OnEnable()
    {
        UIEvents.OnStartButtonClicked += HandleGameStart;
        UIEvents.OnMainMenuButtonClicked += HandleMainMenu;
        GameEvents.OnGameStart += ResetCount;
        GameEvents.OnEnemyDestroyed += CountEnemiesKill;
        UIEvents.OnRewardChanged += HandleRewardPoints;
    }

    private void OnDisable()
    {
        UIEvents.OnStartButtonClicked -= HandleGameStart;
        UIEvents.OnMainMenuButtonClicked -= HandleMainMenu;
        GameEvents.OnGameStart -= ResetCount;
        GameEvents.OnEnemyDestroyed -= CountEnemiesKill;
        UIEvents.OnRewardChanged -= HandleRewardPoints;
    }

    private void HandleGameStart()
    {
        GameEvents.RaiseGameReady();
        currentState = GameState.GamePlaying;
    }

    private void HandleMainMenu(bool canSave)
    {
        if (canSave)
            SaveData.SaveRewardPoints(rewardPoints);
        else
            rewardPoints = SaveData.LoadRewardPoints();
        GameEvents.RaiseGameQuit();
        currentState = GameState.MainMenu;
    }

    private void HandleRewardPoints(int rewardPoints)
    {
        this.rewardPoints = rewardPoints;
    }

    private void ResetCount(bool isStarted)
    {
        if (isStarted)
        {
            enemiesKill = 0;
            bossesKill = 0;
        }
    }

    private void CountEnemiesKill(Enemy enemy)
    {
        if (enemy.IsBoss)
            bossesKill++;
        else
            enemiesKill++;
    }

    private void SetupPlayerDatabase()
    {
        playerDict = new Dictionary<int, bool>();
        for (int i = 0; i < playerDB.Players.Length; i++)
        {
            int unlockedByDefault = playerDB.Players[i].UnlockedByDefault ? 1 : 0;
            bool hasUnlocked = SaveData.LoadShipIsUnlocked(i.ToString(), unlockedByDefault) == 1;

            playerDict.Add(i, hasUnlocked);
        }
    }

    public void UnlockPlayerShip(int index)
    {
        if (playerDict.ContainsKey(index))
            playerDict[index] = true;
    }

    public bool HasUnlockedShip(int index)
    {
        if (playerDict.ContainsKey(index))
            return playerDict[index];

        return false;
    }

    public int RewardPoints => rewardPoints;
    public bool IsGamePlayingState() => currentState == GameState.GamePlaying;
    public int EnemiesKill => enemiesKill;
    public int BossesKill => bossesKill;
    public PlayerDatabase PlayerDB => playerDB;
}
