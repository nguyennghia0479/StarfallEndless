using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private SettingsUI settingsUI;
    [SerializeField] private ReviveUI reviveUI;
    [SerializeField] private CountingUI countingUI;
    [SerializeField] private GameOverUI gameOverUI;

    [Header("Manager Elements")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private WaveManager waveManager;

    private void OnEnable()
    {
        //GameEvents.OnGameStarted += HandleGameStarted;
        GameEvents.OnGameRetry += HandleGameRetry;
        GameEvents.OnPlayerDestroyed += EnableReviveUI;
        UIEvents.OnPlayerRevived += HandlePlayerRevived;
        UIEvents.OnGameEnded += EnableGameOverUI;
    }

    private void OnDisable()
    {
        //GameEvents.OnGameStarted -= HandleGameStarted;
        GameEvents.OnGameRetry -= HandleGameRetry;
        GameEvents.OnPlayerDestroyed -= EnableReviveUI;
        UIEvents.OnPlayerRevived -= HandlePlayerRevived;
        UIEvents.OnGameEnded -= EnableGameOverUI;
    }

    private void Start()
    {
        HandleGameStarted();
    }

    private void HandleGameStarted()
    {
        DisableSetingUI();
        DisableReviveUI();
        DisableGameOverUI();
    }

    private void HandleGameRetry()
    {
        DisableReviveUI();
        DisableGameOverUI();
        countingUI.gameObject.SetActive(true);
        countingUI.SetToCountdown();
    }

    private void HandlePlayerRevived()
    {
        DisableReviveUI();
        countingUI.gameObject.SetActive(true);
        countingUI.SetToCountdown();
    }

    public void EnableSettingUI() => settingsUI.gameObject.SetActive(true);

    public void DisableSetingUI() => settingsUI.gameObject.SetActive(false);

    private void EnableReviveUI() => reviveUI.gameObject.SetActive(true);

    private void DisableReviveUI() => reviveUI.gameObject.SetActive(false);

    private void EnableGameOverUI()
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
        gameOverUI.gameObject.SetActive(true);
    }

    private void DisableGameOverUI() => gameOverUI.gameObject.SetActive(false);
}
