using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject[] uiElements;

    [Header("UI Elements")]
    [SerializeField] private MainMenuUI mainMenuUI;
    [SerializeField] private MainGameUI mainGameUI;
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private ReviveUI reviveUI;
    [SerializeField] private CountingUI countingUI;
    [SerializeField] private GameOverUI gameOverUI;

    [Header("Manager Elements")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private WaveManager waveManager;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameManager);
    }

    private void OnEnable()
    {
        UIEvents.OnGamePlayed += HandleGamePlayed;
        GameEvents.OnGameRetried += HandleGameRetrired;
        GameEvents.OnPlayerDestroyed += HandlePlayerDestroyed;
        UIEvents.OnPlayerRevived += HandlePlayerRevived;
        UIEvents.OnGameEnded += HandleGameEnded;
    }

    private void OnDisable()
    {
        UIEvents.OnGamePlayed -= HandleGamePlayed;
        GameEvents.OnGameRetried -= HandleGameRetrired;
        GameEvents.OnPlayerDestroyed -= HandlePlayerDestroyed;
        UIEvents.OnPlayerRevived -= HandlePlayerRevived;
        UIEvents.OnGameEnded -= HandleGameEnded;
    }

    private void Start()
    {
        HandleMainMenu();
    }

    public void SwitchToUI(GameObject uiToEnable)
    {
        foreach (var ui in uiElements)
            ui.SetActive(false);

        uiToEnable.SetActive(true);
    }

    public void SwitchToMainMenuUI()
    {
        SwitchToUI(mainMenuUI.gameObject);
    }

    public void SwitchToMainGameUI()
    {
        SwitchToUI(mainGameUI.gameObject);
    }

    public void SwitchToSettingUI()
    {
        SwitchToUI(settingsUI.gameObject);
    }

    private void HandleMainMenu()
    {
        SwitchToUI(mainMenuUI.gameObject);
        gameManager.SwitchToMainMenuState();
    }

    private void HandleGamePlayed()
    {
        SwitchToUI(mainGameUI.gameObject);
        gameManager.SwitchToGameState();
        countingUI.gameObject.SetActive(true);
        HandleResetPointsUI();
    }

    private void HandleGameRetrired(bool isRetried)
    {
        if (isRetried)
            HandleGamePlayed();
        else
            HandleMainMenu();

        HandleResetPointsUI();
    }

    private void HandleResetPointsUI()
    {
        mainGameUI.ResetPoints();
    }

    private void HandlePlayerDestroyed()
    {
        SwitchToUI(reviveUI.gameObject);
    }

    private void HandlePlayerRevived()
    {
        HandleGamePlayed();
    }

    private void HandleGameEnded()
    {
        GameResultData resultData = new()
        {
            enemiesKillText = gameManager.EnemiesKill.ToString(),
            bossesKillText = gameManager.BossesKill.ToString(),
            rewardPointsText = scoreManager.RewardPoints.ToString(),
            scorePointsText = scoreManager.ScorePoints.ToString(),
            waveCompletedText = waveManager.CurrentWave.ToString()
        };

        gameOverUI.SetGameResult(resultData);
        SwitchToUI(gameOverUI.gameObject);
    }
}
