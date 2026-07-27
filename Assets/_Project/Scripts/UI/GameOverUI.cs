using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [Header("Result Text")]
    [SerializeField] private TMP_Text enemiesKillText;
    [SerializeField] private TMP_Text bossesKillText;
    [SerializeField] private TMP_Text rewardPointsText;
    [SerializeField] private TMP_Text scorePointsText;
    [SerializeField] private TMP_Text waveCompletedText;

    [Header("Button")]
    [SerializeField] private Button retryButton;
    [SerializeField] private Button mainMenuButton;

    private void OnEnable()
    {
        retryButton.onClick.AddListener(OnRetryButtonClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuButtonClicked);
    }

    private void OnDisable()
    {
        retryButton.onClick.RemoveListener(OnRetryButtonClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuButtonClicked);
    }

    public void SetGameResult(GameResultData resultData)
    {
        enemiesKillText.text = resultData.enemiesKillText;
        bossesKillText.text = resultData.bossesKillText;
        rewardPointsText.text = resultData.rewardPointsText;
        scorePointsText.text = resultData.scorePointsText;
        waveCompletedText.text = resultData.waveCompletedText;
    }

    private void OnRetryButtonClicked()
    {
        UIEvents.RaiseStartButtonClicked();
    }

    private void OnMainMenuButtonClicked()
    {
        UIEvents.RaiseMainMenuButtonClicked();
    }
}

public struct GameResultData
{
    public string enemiesKillText;
    public string bossesKillText;
    public string rewardPointsText;
    public string scorePointsText;
    public string waveCompletedText;
}