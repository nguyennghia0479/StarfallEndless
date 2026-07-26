using UnityEngine;

public enum GameState
{
    MainMenu, GamePlaying, GameOver
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
        GameEvents.OnGameRetried += ResetCount;
        GameEvents.OnEnemyDestroyed += CountEnemiesKill;
    }

    private void OnDisable()
    {
        GameEvents.OnGameRetried -= ResetCount;
        GameEvents.OnEnemyDestroyed -= CountEnemiesKill;
    }

    private void ResetCount(bool _)
    {
        enemiesKill = 0;
        bossesKill = 0;
    }

    private void CountEnemiesKill(Enemy enemy)
    {
        if (enemy.IsBoss)
            bossesKill++;
        else
            enemiesKill++;
    }

    public void SwitchToMainMenuState() => currentState = GameState.MainMenu;
    public void SwitchToGameState() => currentState = GameState.GamePlaying;
    public bool IsMainMenuState() => currentState == GameState.MainMenu;
    public bool IsGamePlayingState() => currentState == GameState.GamePlaying;
    public GameState State => currentState;
    public int EnemiesKill => enemiesKill;
    public int BossesKill => bossesKill;
}
