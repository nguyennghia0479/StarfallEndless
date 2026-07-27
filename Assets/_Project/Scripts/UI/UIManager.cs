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
        GameEvents.OnGameReady += HandleGameReady;
        GameEvents.OnPlayerDestroyed += HandleEnableReviveUI;
        UIEvents.OnReviveButtonClicked += HandlePlayerRevived;
        UIEvents.OnEndGameButtonClicked += HandleEnableGameOverUI;
        GameEvents.OnGameQuit += HandleEnableMainMenuUI;
    }

    private void OnDisable()
    {
        GameEvents.OnGameReady -= HandleGameReady;
        GameEvents.OnPlayerDestroyed -= HandleEnableReviveUI;
        UIEvents.OnReviveButtonClicked -= HandlePlayerRevived;
        UIEvents.OnEndGameButtonClicked -= HandleEnableGameOverUI;
        GameEvents.OnGameQuit -= HandleEnableMainMenuUI;
    }

    private void Start()
    {
        HandleEnableMainMenuUI();
    }

    public void SwitchToUI(GameObject uiToEnable)
    {
        foreach (var ui in uiElements)
            ui.SetActive(false);

        uiToEnable.SetActive(true);
    }

    public void SwitchToSettingUI()
    {
        settingsUI.gameObject.SetActive(true);
    }

    private void HandleGameReady()
    {
        SwitchToUI(mainGameUI.gameObject);
        countingUI.SetStartCountdown(true);
        mainGameUI.ResetPointsUI();
    }

    private void HandleEnableReviveUI()
    {
        reviveUI.gameObject.SetActive(true);
        reviveUI.EnableReviveButton(scoreManager.RewardPoints);
    }

    private void HandlePlayerRevived(int _)
    {
        SwitchToUI(mainGameUI.gameObject);
        countingUI.SetStartCountdown(false);
    }

    private void HandleEnableGameOverUI()
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

    private void HandleEnableMainMenuUI()
    {
        SwitchToUI(mainMenuUI.gameObject);
    }

}
