using UnityEngine;

public enum GameState
{
    MainMenu, GamePlaying
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private GameState currentState = GameState.MainMenu;
    private int enemiesKill;
    private int bossesKill;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void OnEnable()
    {
        UIEvents.OnStartButtonClicked += HandleGameStart;
        UIEvents.OnMainMenuButtonClicked += HandleMainMenu;
        GameEvents.OnGameStart += ResetCount;
        GameEvents.OnEnemyDestroyed += CountEnemiesKill;
    }

    private void OnDisable()
    {
        UIEvents.OnStartButtonClicked -= HandleGameStart;
        UIEvents.OnMainMenuButtonClicked -= HandleMainMenu;
        GameEvents.OnGameStart -= ResetCount;
        GameEvents.OnEnemyDestroyed -= CountEnemiesKill;
    }

    private void HandleGameStart()
    {
        GameEvents.RaiseGameReady();
        currentState = GameState.GamePlaying;
    }

    private void HandleMainMenu()
    {
        GameEvents.RaiseGameQuit();
        currentState = GameState.MainMenu;
    }

    private void ResetCount(bool isRestarted)
    {
        if (isRestarted)
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

    public bool IsGamePlayingState() => currentState == GameState.GamePlaying;
    public int EnemiesKill => enemiesKill;
    public int BossesKill => bossesKill;
}
